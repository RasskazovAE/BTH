using BTH.Core.Context;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MvvmCross.IoC;
using System.Data.Common;

namespace BTH.Tests
{
    public static class ContainerInstaller
    {
        public static void InstallTestDb(this IMvxIoCProvider provider)
        {
            var dataContext = new DataContext(new DbContextOptionsBuilder()
                .UseSqlite(CreateInMemoryDatabase())
                .Options);
            provider.RegisterSingleton(typeof(DataContext), dataContext);
        }

        private static DbConnection CreateInMemoryDatabase()
        {
            var connection = new SqliteConnection("Filename=:memory:");

            connection.Open();

            return connection;
        }
    }
}
