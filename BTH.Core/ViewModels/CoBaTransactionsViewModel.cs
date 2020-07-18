using BHT.Core.Entities;
using BHT.Core.Readers.CoBa;
using BHT.Core.Services.CoBa;
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
        private readonly ICoBaReader _coBaReader;
        private readonly ICoBaService _coBaService;
        
        private ICommand _loadFileCommand;
        public ICommand LoadFileCommand
        {
            get
            {
                _loadFileCommand = _loadFileCommand ?? new MvxCommand(LoadFile);
                return _loadFileCommand;
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
            ICoBaReader coBaReader,
            ICoBaService coBaService)
        {
            _fileDialogExplorer = fileDialogExplorer;
            _coBaReader = coBaReader;
            _coBaService = coBaService;
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
                var transactions = await _coBaReader.ParseCsvFileAsync(fileName);
                await _coBaService.AddNewAsync(transactions);
                await LoadData();
            }
        }

        private async Task LoadData()
        {
            var items = await _coBaService.Get();
            Transactions.Clear();
            Array.ForEach(items, e => Transactions.Add(e));
        }
    }
}
