using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace BTH.WPF.Printing
{
    /// <summary>
    /// Interaction logic for PrintPreview.xaml
    /// </summary>
    public partial class PrintPreview : Window
    {
        public FlowDocument Document { get; set; }

        public PrintPreview()
        {
            InitializeComponent();
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            // Create a PrintDialog  
            PrintDialog printDlg = new PrintDialog();
            // Create IDocumentPaginatorSource from FlowDocument  
            IDocumentPaginatorSource idpSource = Document;
            // Call PrintDocument method to send document to printer  
            if (printDlg.ShowDialog() == true)
                printDlg.PrintDocument(idpSource.DocumentPaginator, "Report name");

            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
