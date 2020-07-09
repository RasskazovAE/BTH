using BHT.WPF.Entities;
using BHT.WPF.Readers.CoBa;
using BHT.WPF.Services.CoBa;
using Microsoft.Extensions.DependencyInjection;

namespace BHT.WPF
{
    public static class ContainerInstaller
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            // services for DI...

            services.AddDbContext<DataContext>();

            services.AddTransient<ICoBaReader, CoBaReader>();
            services.AddTransient<ICoBaService, CoBaService>();

            services.AddTransient<MainWindow>();
        }
    }
}
