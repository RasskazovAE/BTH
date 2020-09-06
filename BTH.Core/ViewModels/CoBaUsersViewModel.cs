using BHT.Core.Readers.CoBa;
using BHT.Core.Services.CoBa.Transactions;
using BTH.Core.Entities;
using BTH.Core.Services.CoBa.Users;
using BTH.Core.ViewModels.Interfaces;
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
        private readonly IFileDialogExplorer _fileDialogExplorer;
        private readonly ICoBaReader _coBaReader;
        private readonly ICoBaTransactionService _coBaService;
        private readonly IMvxNavigationService _navigationService;
        private readonly ICoBaUserService _coBaUserService;

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        private MvxObservableCollection<CoBaUserViewModel> _userViewModels;
        public MvxObservableCollection<CoBaUserViewModel> UserViewModels
        {
            get
            {
                _userViewModels = _userViewModels ?? new MvxObservableCollection<CoBaUserViewModel>();
                return _userViewModels;
            }
        }

        private ICommand _loadFileCommand;
        public ICommand LoadFileCommand
        {
            get
            {
                _loadFileCommand = _loadFileCommand ?? new MvxCommand(LoadFile);
                return _loadFileCommand;
            }
        }

        public CoBaUsersViewModel(
            IFileDialogExplorer fileDialogExplorer,
            ICoBaReader coBaReader,
            ICoBaTransactionService coBaService,
            IMvxNavigationService navigationService,
            ICoBaUserService coBaUserService)
        {
            _fileDialogExplorer = fileDialogExplorer;
            _coBaReader = coBaReader;
            _coBaService = coBaService;
            _navigationService = navigationService;
            _coBaUserService = coBaUserService;
        }

        public override async void ViewAppeared()
        {
            base.ViewAppeared();

            IsLoading = true;
            try
            {
                await LoadData();
            }
            finally
            {
                IsLoading = false;
            }
            
        }

        private async void LoadFile()
        {
            var fileName = _fileDialogExplorer.OpenFileDialog();
            if (!string.IsNullOrEmpty(fileName))
            {
                IsLoading = true;
                try
                {
                    var transactionsCsv = await _coBaReader.ParseCsvFileAsync(fileName);
                    var transactions = await _coBaService.GroupTransactions(transactionsCsv);
                    await _coBaService.AddNewAsync(transactions);
                    await LoadData();
                }
                finally
                {
                    IsLoading = false;
                }
            }
        }

        private async Task LoadData()
        {
            var items = await _coBaUserService.GetAll();
            foreach(var vm in UserViewModels)
            {
                vm.UserClicked -= UserLogin;
            }
            UserViewModels.Clear();
            Array.ForEach(items, e =>
            {
                var vm = new CoBaUserViewModel(e, _coBaUserService);
                vm.UserClicked += UserLogin;
                UserViewModels.Add(vm);
            });
        }

        private async void UserLogin(object sender, EventArgs e)
        {
            var user = sender as CoBaUser;
            if(user != null)
            {
                await _navigationService.Navigate<CoBaTransactionsViewModel, CoBaUser>(user);
            }
        }
    }
}
