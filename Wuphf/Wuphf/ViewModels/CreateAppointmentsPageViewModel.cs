using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Extensions.Logging;
using Wuphf.MVVM;
using Wuphf.Repository.Appointments;
using Wuphf.Shared.Appointments;

namespace Wuphf.ViewModels
{
    public class CreateAppointmentsPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        RegionService regionService;
        public ICommand AppearingCommand { get; set; }
        public ICommand EditCommand { get; set; }
        ILogger<CreateAppointmentsPageViewModel> logger;

        private IServiceProvider serviceProvider;
        public IServiceProvider ServiceProvider
        {
            get { return serviceProvider; }
            set { serviceProvider = value; OnPropertyChanged(); }
        }
        private IAppointmentsRepo appointmentsRepo;
        public IAppointmentsRepo AppointmentsRepo
        {
            get { return appointmentsRepo; }
            set { appointmentsRepo = value; OnPropertyChanged(); }
        }
        public CreateAppointmentsPageViewModel(IServiceProvider serviceProvider
            , RegionService regionService
            , ILogger<CreateAppointmentsPageViewModel> logger
            , IAppointmentsRepo appointmentsRepo
            )
        {
            this.ServiceProvider = serviceProvider;
            this.regionService = regionService;
            this.logger = logger;
            AppointmentsRepo = appointmentsRepo;
            AppearingCommand = new DelegateCommand(OnAppearing);
            EditCommand = new DelegateCommand(OnEdit);
        }
        public async void OnAppearing()
        {
            await AppointmentsRepo.GetAppointments();
            
        }
        public async void OnEdit()
        {
            await App.Current.MainPage.DisplayAlert("YourApp", "EDIT", "Ok");

        }
    }
}
