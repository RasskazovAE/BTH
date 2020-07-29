using BHT.Core.Entities;
using BHT.Core.Readers.CoBa;
using BHT.Core.Services.CoBa;
using BTH.Core.Dto;
using BTH.Core.ViewModels.Interfaces;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BTH.Core.ViewModels
{
    public class CoBaTransactionsViewModel : MvxViewModel
    {
        private readonly IFileDialogExplorer _fileDialogExplorer;
        private readonly IPrintService _printService;
        private readonly ICoBaReader _coBaReader;
        private readonly ICoBaService _coBaService;

        private Filter _filter;
        public Filter Filter
        {
            get
            {
                _filter = _filter ?? new Filter();
                return _filter;
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
            IFileDialogExplorer fileDialogExplorer,
            IPrintService printService,
            ICoBaReader coBaReader,
            ICoBaService coBaService)
        {
            _fileDialogExplorer = fileDialogExplorer;
            _printService = printService;
            _coBaReader = coBaReader;
            _coBaService = coBaService;
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

        private async void LoadFile()
        {
            var fileName = _fileDialogExplorer.OpenFileDialog();
            if (!string.IsNullOrEmpty(fileName))
            {
                var transactions = await _coBaReader.ParseCsvFileAsync(fileName);
                await _coBaService.AddNewAsync(transactions);
                await LoadData();
            }
        }

        private async Task LoadData()
        {
            // todo разобраться с передаваемым фильтром
            var items = await _coBaService.Get(Filter);
            Transactions.Clear();
            Array.ForEach(items, e => Transactions.Add(e));
        }

        private void Print()
        {
            _printService.Print();
        }
    }
}
