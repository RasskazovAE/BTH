using BHT.Core.Entities;
using BTH.Core.Dto;
using BTH.WPF.DocumentGenerators.CoBa;
using MvvmCross.Commands;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;

namespace BTH.WPF.Printing
{
    public class PrintPreviewViewModel : BaseNotifyObject
    {
        private CoBaTransaction[] _transactions;
        private PrintServer _printServer;
        private PageMediaSize _selectedPageMediaSize;

        public event EventHandler Closing;
        private void OnClosing()
        {
            Closing?.Invoke(this, EventArgs.Empty);
        }

        private MvxCommand _printCommand;
        public MvxCommand PrintCommand
        {
            get
            {
                _printCommand = _printCommand ?? new MvxCommand(Print, () => Document != null);
                return _printCommand;
            }
        }

        private ICommand _cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                _cancelCommand = _cancelCommand ?? new MvxCommand(OnClosing);
                return _cancelCommand;
            }
        }

        public IDocumentPaginatorSource Document
        {
            get => Get<IDocumentPaginatorSource>();
            set
            {
                Set(value);
                PrintCommand.RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<PrintQueue> PrintQueues { get; set; }
        public PrintQueue SelectedPrintQueue
        {
            get => Get<PrintQueue>();
            set
            {
                Set(value);
                PrintQueueChanged();
            }
        }

        public PrintPreviewViewModel(CoBaTransaction[] transactions)
        {
            PrintQueues = new ObservableCollection<PrintQueue>();

            _transactions = transactions;
            LoadData();
        }

        public void LoadData()
        {
            _printServer = new PrintServer();
            foreach (var printQueue in _printServer.GetPrintQueues())
            {
                PrintQueues.Add(printQueue);
            }
        }

        private void PrintQueueChanged()
        {
            _selectedPageMediaSize = SelectedPrintQueue.GetPrintCapabilities().PageMediaSizeCapability
                .FirstOrDefault(e => e.PageMediaSizeName.HasValue && e.PageMediaSizeName.Value == PageMediaSizeName.ISOA4);
            if (_selectedPageMediaSize.Width.HasValue && _selectedPageMediaSize.Height.HasValue)
                BuildDocument(_selectedPageMediaSize.Width.Value, _selectedPageMediaSize.Height.Value);
            else
                Document = null;
        }

        private void BuildDocument(double pageWidth, double pageHeight)
        {
            var paginator = new CoBaPaginator(_transactions, new Typeface("Arial"), 10, 96 * 0.75, new Size(pageWidth, pageHeight));
            using (var ms = new MemoryStream())
            {
                Package package = Package.Open(ms, FileMode.Create, FileAccess.ReadWrite);
                Uri uri = new Uri("pack://document.xps");
                PackageStore.RemovePackage(uri);
                PackageStore.AddPackage(uri, package);
                XpsDocument xpsDocument = new XpsDocument(package, CompressionOption.Fast, uri.AbsoluteUri);
                XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(xpsDocument);
                writer.Write(paginator);

                Document = xpsDocument.GetFixedDocumentSequence();
                CommandManager.InvalidateRequerySuggested();
            }
        }

        private void Print()
        {
            PrintDialog printDialog = new PrintDialog();
            printDialog.PrintQueue = SelectedPrintQueue;
            printDialog.PrintTicket.PageMediaSize = _selectedPageMediaSize;
            printDialog.PrintDocument(Document.DocumentPaginator, "");
            OnClosing();
        }
    }
}
