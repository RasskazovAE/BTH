using BHT.Core.Entities;
using MvvmCross.Platforms.Wpf.Views;
using System.ComponentModel;

namespace BTH.WPF.Printing
{
    /// <summary>
    /// Interaction logic for PrintPreview.xaml
    /// </summary>
    public partial class PrintPreview : MvxWindow
    {
        public PrintPreview(CoBaTransaction[] transactions)
        {
            InitializeComponent();

            var vm = new PrintPreviewViewModel(transactions);
            vm.PropertyChanged += Vm_PropertyChanged;
            vm.Closing += (s, e) => Close();
            DataContext = vm;
        }

        private void Vm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var vm = sender as PrintPreviewViewModel;
            if (vm != null &&
                e.PropertyName == nameof(PrintPreviewViewModel.Document) &&
                vm.Document != null)
            {
                Viewer.FitToWidth();
            }
        }
    }
}
