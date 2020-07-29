using BTH.Core.ViewModels.Interfaces;
using Microsoft.Win32;

namespace BTH.WPF.Services
{
    public class FileDialogExplorer : IFileDialogExplorer
    {
        public string OpenFileDialog(string initialPath = null)
        {
            var dlg = new OpenFileDialog();
            dlg.Filter = "csv|*.csv";
            if (dlg.ShowDialog() == true)
            {
                return dlg.FileName;
            }
            return null;
        }
    }
}
