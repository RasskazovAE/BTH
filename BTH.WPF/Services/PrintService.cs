using BHT.Core.Entities;
using BTH.Core.ViewModels.Interfaces;
using BTH.WPF.DocumentGenerators.CoBa;
using BTH.WPF.Printing;
using System;
using System.IO;
using System.IO.Packaging;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;

namespace BTH.WPF.Services
{
    public class PrintService : IPrintService
    {
        public void Print(CoBaTransaction[] transactions)
        {
            // Create a PrintDialog
            PrintDialog printDlg = new PrintDialog();
            // Call PrintDocument method to send document to printer  
            if (printDlg.ShowDialog() == true)
            {
                var paginator = new CoBaPaginator(transactions, new Typeface("Arial"), 10, 96 * 0.75, new Size(printDlg.PrintableAreaWidth, printDlg.PrintableAreaHeight));
                using (var ms = new MemoryStream())
                {
                    Package package = Package.Open(ms, FileMode.Create, FileAccess.ReadWrite);
                    Uri uri = new Uri("pack://document.xps");
                    PackageStore.AddPackage(uri, package);
                    XpsDocument xpsDocument = new XpsDocument(package, CompressionOption.Fast, uri.AbsoluteUri);
                    XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(xpsDocument);
                    writer.Write(paginator);

                    var printPreview = new PrintPreview();
                    printPreview.Owner = Application.Current.MainWindow;
                    printPreview.Viewer.Document = xpsDocument.GetFixedDocumentSequence();
                    printPreview.Viewer.FitToWidth();
                    printPreview.Show();
                }
            }
        }
    }
}
