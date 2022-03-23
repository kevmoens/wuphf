using System;
using NLog.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Neleus.DependencyInjection.Extensions;
using Wuphf.MVVM;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Wuphf.Shared.Appointments;
using System.Net.Http;

namespace Wuphf
{
    public partial class App : Xamarin.Forms.Application
    {
        internal static IServiceProvider ServiceProvider { get; set; }
        public App()
        {
            InitializeComponent();

            ConfigureServices();
            ViewModelLocator.ServiceProvider = ServiceProvider;
            MainPage = new NavigationPage(new Views.HomePage());
            RegionManager.SetRegionName(MainPage, "MainRegion");
            RegionManager.SetServiceProvider(MainPage, ServiceProvider);
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
                .AddScoped(sp => new HttpClient { BaseAddress = new Uri(Wuphf.Application.AppSettingsManager.Settings["WuphfUrl"].TrimEnd('/')) })
                .AddTransient<IAppointmentsRepo, AppointmentsRepo>()
                .AddTransient<Views.AppointmentsPage>()
                .AddTransient<Views.CreateAppointmentsPage>()
                .AddTransient<Views.MainPage>()
                .AddTransient<Views.HomePage>()
                .AddTransient<ViewModels.AppointmentsPageViewModel>()
                .AddTransient<ViewModels.CreateAppointmentsPageViewModel>()
                .AddTransient<ViewModels.HomePageViewModel>()
                .AddTransient<ViewModels.MainPageViewModel>()
            ;
            services.AddByName<Page>()
                .Add<Views.MainPage>("Main")
                .Add<Views.AppointmentsPage>("Appointments")
                .Add<Views.HomePage>("Home")
                .Add<Views.CreateAppointmentsPage>("CreateAppointments")
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
