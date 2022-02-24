using System;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
            HttpClient client = new HttpClient(); // { BaseAddress = new Uri("https://localhost:5003") };
            LoginRequest login = new LoginRequest();
            login.UserName = UserName;
            login.Password = Password;
            var loginJson = Newtonsoft.Json.JsonConvert.SerializeObject(login);
            var result = await client.PostAsync($"http://localhost:5002/Account/{UserName}", new StringContent(loginJson, Encoding.UTF8, "application/json"));
            if (!result.IsSuccessStatusCode)
            {
                System.Diagnostics.Debug.Print(result.ReasonPhrase);
                return;
            }
            var content = await result.Content.ReadAsStringAsync();
            Guid token = Guid.Empty;
            if (!string.IsNullOrEmpty(content) && !Guid.TryParse(content, out token))
            {
                await Application.Current.MainPage.DisplayAlert("Error",content, "OK");
                return;
            }
            regionService?.Navigate("MainRegion", "Appointments");
        }
    }
}
