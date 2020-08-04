using BHT.Core.Entities;
using BTH.Core.ViewModels.Interfaces;
using BTH.WPF.Printing;
using System.Windows;

namespace BTH.WPF.Services
{
    public class PrintService : IPrintService
    {
        public void Print(CoBaTransaction[] transactions)
        {
            var printPreview = new PrintPreview(transactions);
            printPreview.Owner = Application.Current.MainWindow;
            printPreview.Show();
        }
    }
}
