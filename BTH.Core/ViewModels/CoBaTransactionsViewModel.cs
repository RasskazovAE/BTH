using BHT.Core.Entities;
using BHT.Core.Services.CoBa.Transactions;
using BTH.Core.Dto;
using BTH.Core.Entities;
using BTH.Core.ViewModels.Interfaces;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BTH.Core.ViewModels
{
    public class CoBaTransactionsViewModel : MvxViewModel<CoBaUser>
    {
        private CoBaUser _user;

        private readonly IMvxNavigationService _navigationService;
        private readonly ICoBaTransactionService _coBaService;
        private readonly IPrintService _printService;

        private Filter _filter;
        public Filter Filter
        {
            get
            {
                _filter = _filter ?? new Filter();
                return _filter;
            }
        }

        private ICommand _goBackCommand;
        public ICommand GoBackCommand
        {
            get
            {
                _goBackCommand = _goBackCommand ?? new MvxCommand(GoBack);
                return _goBackCommand;
            }
        }

        private ICommand _printCommend;
        public ICommand PrintCommand
        {
            get
            {
                _printCommend = _printCommend ?? new MvxCommand(Print);
                return _printCommend;
            }
        }

        private ICommand _filterApplyCommand;
        public ICommand FilterApplyCommand
        {
            get
            {
                _filterApplyCommand = _filterApplyCommand ?? new MvxCommand(ApplyFilter);
                return _filterApplyCommand;
            }
        }

        private MvxObservableCollection<CoBaTransaction> _transactions;
        public MvxObservableCollection<CoBaTransaction> Transactions
        {
            get
            {
                _transactions = _transactions ?? new MvxObservableCollection<CoBaTransaction>();
                return _transactions;
            }
        }

        public CoBaTransactionsViewModel(
            IMvxNavigationService navigationService,
            ICoBaTransactionService coBaService,
            IPrintService printService)
        {
            _navigationService = navigationService;
            _printService = printService;
            _coBaService = coBaService;
        }

        public override void Prepare(CoBaUser coBaUser)
        {
            _user = coBaUser;
        }

        public override async void ViewAppeared()
        {
            base.ViewAppeared();

            await LoadData();
        }

        private async void ApplyFilter()
        {
            //todo validate filter
            await LoadData();
        }

        private async Task LoadData()
        {
            // todo разобраться с передаваемым фильтром
            var items = await _coBaService.Get(_user, Filter);
            Transactions.Clear();
            Array.ForEach(items, e => Transactions.Add(e));
        }

        private void Print()
        {
            _printService.Print(Transactions.ToArray());
        }

        private async void GoBack()
        {
            await _navigationService.Navigate<CoBaUsersViewModel>();
        }
    }
}
