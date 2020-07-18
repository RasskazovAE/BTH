using BHT.Core.Readers.CoBa;
using BHT.Core.Services.CoBa;
using BTH.Core.Context;
using MvvmCross.IoC;

namespace BTH.Core
{
    public static class ConrainerInstaller
    {
        public static void InstallDb(this IMvxIoCProvider provider)
        {
            provider.RegisterType<DataContext>();
        }

        public static void InstallInterfaces(this IMvxIoCProvider provider)
        {
            provider.RegisterType<ICoBaReader, CoBaReader>();
            provider.RegisterType<ICoBaService, CoBaService>();
        }
    }
}
