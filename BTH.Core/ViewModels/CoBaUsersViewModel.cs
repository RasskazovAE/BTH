using BTH.Core.Entities;
using BTH.Core.Services.CoBa.Users;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BTH.Core.ViewModels
{
    public class CoBaUsersViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly ICoBaUserService _coBaUserService;

        private MvxObservableCollection<CoBaUser> _users;
        public MvxObservableCollection<CoBaUser> Users
        {
            get
            {
                _users = _users ?? new MvxObservableCollection<CoBaUser>();
                return _users;
            }
        }

        private ICommand _userLoginCommand;
        public ICommand UserLoginCommand
        {
            get
            {
                _userLoginCommand = _userLoginCommand ?? new MvxCommand<CoBaUser>(UserLogin);
                return _userLoginCommand;
            }
        }

        public CoBaUsersViewModel(
            IMvxNavigationService navigationService,
            ICoBaUserService coBaUserService)
        {
            _navigationService = navigationService;
            _coBaUserService = coBaUserService;
        }

        public override async void ViewAppeared()
        {
            base.ViewAppeared();

            await LoadData();
        }

        private async Task LoadData()
        {
            var items = await _coBaUserService.GetAll();
            Users.Clear();
            Array.ForEach(items, e => Users.Add(e));
        }

        private async void UserLogin(CoBaUser user)
        {
            await _navigationService.Navigate<CoBaTransactionsViewModel>();
        }
    }
}
