using BHT.Core.Entities;
using BTH.Core.ViewModels.Interfaces;
using BTH.WPF.Printing;
using System.Windows;
using System.Windows.Documents;

namespace BTH.WPF.Services
{
    public class PrintService : IPrintService
    {
        public void Print(CoBaTransaction[] transactions)
        {
            var printPreview = new PrintPreview();
            printPreview.Owner = Application.Current.MainWindow;
            printPreview.Document = CreateDocument();
            printPreview.Show();
        }

        private FlowDocument CreateDocument()
        {
            var doc = new FlowDocument();
            doc.PageHeight = 736;
            doc.PageWidth = 512;

            // Create a Section  
            Section sec = new Section();
            // Create first Paragraph  
            Paragraph p1 = new Paragraph();
            // Create and add a new Bold, Italic and Underline  
            Bold bld = new Bold();
            var run = new Run("Some template");
            bld.Inlines.Add(run);
            Italic italicBld = new Italic();
            italicBld.Inlines.Add(bld);
            Underline underlineItalicBld = new Underline();
            underlineItalicBld.Inlines.Add(italicBld);
            // Add Bold, Italic, Underline to Paragraph  
            p1.Inlines.Add(underlineItalicBld);
            // Add Paragraph to Section  
            sec.Blocks.Add(p1);
            // Add Section to FlowDocument  
            doc.Blocks.Add(sec);

            return doc;
        }
    }
}
