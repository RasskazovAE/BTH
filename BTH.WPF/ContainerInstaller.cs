using BankTransactionHistory.Entities;
using BankTransactionHistory.Readers.CoBa;
using BankTransactionHistory.Services.CoBa;
using Microsoft.Extensions.DependencyInjection;

namespace BankTransactionHistory
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
