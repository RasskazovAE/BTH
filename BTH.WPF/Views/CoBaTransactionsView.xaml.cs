using BHT.Core.Readers.CoBa;
using BHT.Core.Services.CoBa;
using BTH.Core.Dto;
using BTH.Core.ViewModels;
using BTH.Core.ViewModels.Interfaces;
using BTH.IoC;
using MvvmCross.Platforms.Wpf.Views;

namespace BTH.WPF.Views
{
    /// <summary>
    /// Interaction logic for CoBaTransactionsView.xaml
    /// </summary>
    public partial class CoBaTransactionsView : MvxWpfView
    {
        public CoBaTransactionsView()
        {
            InitializeComponent();
            this.DataContext = new CoBaTransactionsViewModel(
                Manager.Instance.Container.Resolve<IFileDialogExplorer>(),
                Manager.Instance.Container.Resolve<ICoBaReader>(),
                Manager.Instance.Container.Resolve<ICoBaService>()
                ) ;
        }
    }
}
