using System;
using NLog.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Neleus.DependencyInjection.Extensions;
using Wuphf.MVVM;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wuphf
{
    public partial class App : Application
    {
        internal static IServiceProvider ServiceProvider { get; set; }
        public App()
        {
            InitializeComponent();

            ConfigureServices();
            ViewModelLocator.ServiceProvider = ServiceProvider;
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }


        public static void ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();
            services
                .AddSingleton<RegionService>()
            ;
            services.AddByName<Page>()
                //.Add<Views.MainWindow>("MainWindow")
                //.Add<Views.Connections>("Connections")
                //.Add<Views.ConnectionNew>("ConnectionNew")
                //.Add<Views.ConnectionEditFileSystem>("ConnectionEditFileSystem")
                .Build()
                ;

            ServiceProvider =
                services
                .AddLogging(options =>
                {
                    options.AddNLog();
                })
                .BuildServiceProvider();

        }
    }
}
