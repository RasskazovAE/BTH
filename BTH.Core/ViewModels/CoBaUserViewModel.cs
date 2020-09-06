using BTH.Core.Entities;
using BTH.Core.Services.CoBa.Users;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace BTH.Core.ViewModels
{
    public class CoBaUserViewModel : INotifyPropertyChanged
    {
        private readonly ICoBaUserService _coBaUserService;

        public event EventHandler UserClicked;

        private CoBaUser _user;
        public CoBaUser User
        {
            get => _user;
            private set
            {
                _user = value;
                OnPropertyChange(nameof(User));
            }
        }

        private ICommand _userLoginCommand;
        public ICommand UserLoginCommand
        {
            get
            {
                _userLoginCommand = _userLoginCommand ?? new MvxCommand(UserLogin);
                return _userLoginCommand;
            }
        }

        private ICommand _userSaveCommand;
        public ICommand UserSaveCommand
        {
            get
            {
                _userSaveCommand = _userSaveCommand ?? new MvxCommand(UserSave);
                return _userSaveCommand;
            }
        }

        public CoBaUserViewModel(
            CoBaUser user,
            ICoBaUserService coBaUserService)
        {
            User = user;
            _coBaUserService = coBaUserService;
        }

        private async void UserSave()
        {
            await _coBaUserService.Update(User);
        }

        private void UserLogin()
        {
            UserClicked?.Invoke(User, EventArgs.Empty);
        }

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChange(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        #endregion
    }
}
