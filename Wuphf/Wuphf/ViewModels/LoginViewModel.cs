using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Extensions.Logging;
using Wuphf.MVVM;
using Wuphf.Shared;
using Xamarin.Forms;
using System.Threading.Tasks;
using Wuphf.Shared.Dialog;
using Wuphf.Shared.Repository.Login;
using Microsoft.Extensions.DependencyInjection;

namespace Wuphf.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        IRegionService regionService;
        ILogger<LoginViewModel> logger;
        IMsgBox msgBox;

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
        public LoginViewModel(IServiceProvider serviceProvider
            , IRegionService regionService
            , ILogger<LoginViewModel> logger
            , IMsgBox msgBox
            )
        {
            this.ServiceProvider = serviceProvider;
            this.regionService = regionService;
            this.logger = logger;
            this.msgBox = msgBox;
            LoginCommand = new DelegateCommand(async () => { await OnLogin(); });
        }
        public async Task OnLogin()
        {
            Guid token = Guid.Empty;
            Login login = ServiceProvider.GetService<Login>();
            try
            {
                await login.Process(new LoginRequest() { UserName = UserName, Password = Password });
            } catch (Exception ex)
            {
                await msgBox.Show(ex.Message, "Error", DialogButtons.OK);
                return;
            }
            await regionService?.Navigate("MainRegion", "AppointmentDetails");

        }
    }
}
