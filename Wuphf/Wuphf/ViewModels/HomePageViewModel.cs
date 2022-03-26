using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Extensions.Logging;
using Wuphf.Repository.Login;
using Wuphf.MVVM;
using Wuphf.Shared;
using Xamarin.Forms;

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
        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; OnPropertyChanged(); }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; OnPropertyChanged(); }
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
        public async void OnLogin()
        {
            Guid token = Guid.Empty;
            Login login = new Login();
            try
            {
                await login.Process(new LoginRequest() { UserName = UserName, Password = Password });
            } catch (Exception ex)
            {
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                return;
            }
            var navigation = regionService.NavigationServices["MainRegion"];
            Page page = navigation.NavigationStack[navigation.NavigationStack.Count - 1];
            await regionService?.Navigate("MainRegion", "AppointmentDetails");
            navigation.RemovePage(page);
        }
    }
}
