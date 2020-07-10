using BTH.Core.ViewModels;
using MvvmCross;
using MvvmCross.ViewModels;

namespace BTH.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            Mvx.IoCProvider.Install();

            RegisterAppStart<CoBaTransactionsViewModel>();
        }
    }
}
