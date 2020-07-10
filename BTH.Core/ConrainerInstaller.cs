using BHT.Core.Entities;
using BHT.Core.Readers.CoBa;
using BHT.Core.Services.CoBa;
using MvvmCross.IoC;

namespace BTH.Core
{
    public static class ConrainerInstaller
    {
        public static void Install(this IMvxIoCProvider provider)
        {
            provider.RegisterType(typeof(DataContext));

            provider.RegisterType<ICoBaReader, CoBaReader>();
            provider.RegisterType<ICoBaService, CoBaService>();
        }
    }
}
