using BankTransactionHistory;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BTH.Tests
{
    public class TestBase
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public TestBase()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.ConfigureServices();
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}
