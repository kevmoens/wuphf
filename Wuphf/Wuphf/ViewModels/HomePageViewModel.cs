using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Wuphf.MVVM;
namespace Wuphf.ViewModels
{
    public class HomePageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        RegionService regionService;
        ILogger<HomePageViewModel> logger;

        private IServiceProvider serviceProvider;
        public IServiceProvider ServiceProvider
        {
            get { return serviceProvider; }
            set { serviceProvider = value; OnPropertyChanged(); }
        }
        public ICommand LoginCommand { get; set; }
        public HomePageViewModel(IServiceProvider serviceProvider
            , RegionService regionService
            , ILogger<HomePageViewModel> logger
            )
        {
            this.ServiceProvider = serviceProvider;
            this.regionService = regionService;
            this.logger = logger;
            LoginCommand = new DelegateCommand(OnLogin);
        }
        public void OnLogin()
        {
            regionService?.Navigate("MainRegion", "Appointments");
        }
    }
}
