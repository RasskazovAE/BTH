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
            Mvx.IoCProvider.Install();
            Manager.Instance.Container = Mvx.IoCProvider;

            RegisterAppStart<CoBaTransactionsViewModel>();
        }
    }
}
