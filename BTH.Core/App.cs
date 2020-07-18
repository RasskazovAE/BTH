using BTH.Core.ViewModels;
using BTH.IoC;
using MvvmCross;
using MvvmCross.ViewModels;

namespace BTH.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            Mvx.IoCProvider.InstallDb();
            Mvx.IoCProvider.InstallInterfaces();
            Manager.Instance.Container = Mvx.IoCProvider;

            RegisterAppStart<CoBaTransactionsViewModel>();
        }
    }
}
