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

        private MvxObservableCollection<CoBaUser> _users;
        public MvxObservableCollection<CoBaUser> Users
        {
            get
            {
                _users = _users ?? new MvxObservableCollection<CoBaUser>();
                return _users;
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

            await LoadData();
        }

        private async void LoadFile()
        {
            var fileName = _fileDialogExplorer.OpenFileDialog();
            if (!string.IsNullOrEmpty(fileName))
            {
                var transactionsCsv = await _coBaReader.ParseCsvFileAsync(fileName);
                var transactions = await _coBaService.GroupTransactions(transactionsCsv);
                await _coBaService.AddNewAsync(transactions);
                await LoadData();
            }
        }

        private async Task LoadData()
        {
            var items = await _coBaUserService.GetAll();
            Users.Clear();
            Array.ForEach(items, e => Users.Add(e));
        }

        private async void UserLogin(CoBaUser user)
        {
            await _navigationService.Navigate<CoBaTransactionsViewModel, CoBaUser>(user);
        }
    }
}
