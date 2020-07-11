using MvvmCross.IoC;
using System;

namespace BTH.IoC
{
    public class Manager
    {
        #region Singelton

        private static readonly Lazy<Manager> _lazy = new Lazy<Manager>(() => new Manager());
        public static Manager Instance { get { return _lazy.Value; } }
        private Manager() { }

        #endregion

        private bool _initialized;

        public event EventHandler Initialized;

        private IMvxIoCProvider _container;
        public IMvxIoCProvider Container
        {
            get
            {
                if (!_initialized) throw new InvalidOperationException("Container is not initialized.");
                return _container;
            }
            set
            {
                if (value != null)
                {
                    _container = value;
                    _initialized = true;
                    OnInitialized();
                }
            }
        }

        private void OnInitialized()
        {
            Initialized?.Invoke(this, EventArgs.Empty);
        }
    }
}
