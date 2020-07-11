using BHT.Core.Readers.CoBa;
using BHT.Core.Services.CoBa;
using BTH.Core.ViewModels.Interfaces;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
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

        public CoBaTransactionsViewModel(
            IFileDialogExplorer fileDialogExplorer,
            ICoBaReader coBaReader,
            ICoBaService coBaService)
        {
            _fileDialogExplorer = fileDialogExplorer;
            _coBaReader = coBaReader;
            _coBaService = coBaService;
        }

        private async void LoadFile()
        {
            var fileName = _fileDialogExplorer.OpenFileDialog();
            var transactions = await _coBaReader.ParseCsvFileAsync(fileName);
            await _coBaService.AddOnlyNewAsync(transactions);
        }
    }
}
