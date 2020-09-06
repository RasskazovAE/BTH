using System.Windows;
using System.Windows.Controls;

namespace BTH.WPF.Controls
{
    /// <summary>
    /// Interaction logic for LoadingPanel.xaml
    /// </summary>
    public partial class LoadingPanel : UserControl
    {
        // Dependency Property
        public static readonly DependencyProperty IsLoadingProperty =
             DependencyProperty.Register("IsLoading", typeof(bool),
             typeof(LoadingPanel), new FrameworkPropertyMetadata(false));

        // .NET Property wrapper
        public bool IsLoading
        {
            get { return (bool)GetValue(IsLoadingProperty); }
            set { SetValue(IsLoadingProperty, value); }
        }

        public LoadingPanel()
        {
            InitializeComponent();
        }
    }
}
