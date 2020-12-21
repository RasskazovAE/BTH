using BTH.Core.ViewModels.Interfaces;
using BTH.WPF.Services;
using MvvmCross.IoC;

namespace BTH.WPF
{
    public static class ConrainerInstaller
    {
        public static void Install(this IMvxIoCProvider provider)
        {
            provider.RegisterType<IFileDialogExplorer, FileDialogExplorer>();
            provider.RegisterType<IPrintService, PrintService>();
            provider.RegisterType<INotificationService, NotificationService>();
        }
    }
}
