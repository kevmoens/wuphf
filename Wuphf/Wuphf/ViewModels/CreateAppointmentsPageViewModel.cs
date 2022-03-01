using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Extensions.Logging;
using Wuphf.MVVM;
using Wuphf.Repository.Appointments;

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
        ILogger<CreateAppointmentsPageViewModel> logger;

        private ObservableCollection<Wuphf.Shared.Appointments.Appointment> appointments = new ObservableCollection<Wuphf.Shared.Appointments.Appointment>();
        public ObservableCollection<Wuphf.Shared.Appointments.Appointment> Appointments
        {
            get { return appointments; }
            set { appointments = value; OnPropertyChanged(); }
        }
        private IServiceProvider serviceProvider;
        public IServiceProvider ServiceProvider
        {
            get { return serviceProvider; }
            set { serviceProvider = value; OnPropertyChanged(); }
        }
        public CreateAppointmentsPageViewModel(IServiceProvider serviceProvider
            , RegionService regionService
            , ILogger<CreateAppointmentsPageViewModel> logger
            )
        {
            this.ServiceProvider = serviceProvider;
            this.regionService = regionService;
            this.logger = logger;
            AppearingCommand = new DelegateCommand(OnAppearing);
        }
        public async void OnAppearing()
        {
            var repo = new Appointment();
            Appointments.Clear();
            foreach (var apt in await repo.List())
            {
                Appointments.Add(apt);
            }
        }
    }
}
