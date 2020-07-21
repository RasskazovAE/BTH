using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BTH.Core.Dto
{
    public class BaseNotifyObject : INotifyPropertyChanged
    {
        private Dictionary<string, dynamic> _values = new Dictionary<string, dynamic>();

        protected T Get<T>([CallerMemberName] string propertyName = "")
        {
            if (string.IsNullOrEmpty(propertyName)) return default;
            if (!_values.ContainsKey(propertyName))
                _values[propertyName] = default(T);
            return _values[propertyName];
        }

        protected void Set<T>(T value, [CallerMemberName] string propertyName = "")
        {
            if (string.IsNullOrEmpty(propertyName)) return;
            if (!_values.ContainsKey(propertyName) ||
                _values[propertyName] != value)
            {
                _values[propertyName] = value;
                OnPropertyRaised(propertyName);
            }
        }

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyRaised([CallerMemberName] string propertyname = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        #endregion
    }
}
