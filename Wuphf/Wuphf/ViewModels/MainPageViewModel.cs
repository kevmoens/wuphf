using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Wuphf.MVVM;

namespace Wuphf.ViewModels
{
    public class MainPageViewModel 
    {
        IRegionService regionService;
        ILogger<MainPageViewModel> logger;

        private IServiceProvider serviceProvider;
        public IServiceProvider ServiceProvider
        {
            get { return serviceProvider; }
            set { serviceProvider = value;  }
        }
        public ICommand AppearingCommand { get; set; }
        public MainPageViewModel(IServiceProvider serviceProvider
            , IRegionService regionService
            , ILogger<MainPageViewModel> logger
            )
        {
            this.ServiceProvider = serviceProvider;
            this.regionService = regionService;
            this.logger = logger;
            AppearingCommand = new DelegateCommand(OnAppearing);
        }
        public async void OnAppearing()
        {
            await regionService?.Navigate("MainRegion", "Login");
        }
    }
}
