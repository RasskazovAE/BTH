using BTH.IoC;
using BTH.WPF;
using Microsoft.Extensions.Configuration;
using MvvmCross.Core;
using MvvmCross.Platforms.Wpf.Core;
using MvvmCross.Platforms.Wpf.Views;
using System;
using System.IO;
using System.Windows;

namespace BHT.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : MvxApplication
    {
        public IConfiguration Configuration { get; private set; }

        protected override void RegisterSetup()
        {
            Manager.Instance.Initialized += Container_Initialized;
            this.RegisterSetupType<MvxWpfSetup<BTH.Core.App>>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();
        }

        private void Container_Initialized(object sender, EventArgs e)
        {
            var manager = sender as Manager;
            manager.Container.Install();
        }
    }
}
