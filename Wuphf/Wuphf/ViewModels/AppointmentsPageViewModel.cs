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
    public class AppointmentsPageViewModel : INotifyPropertyChanged
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

        private ObservableCollection<AppointmentDetail> appointmentDetails = new ObservableCollection<AppointmentDetail>();
        public ObservableCollection<AppointmentDetail> AppointmentDetails
        {
            get { return appointmentDetails; }
            set { appointmentDetails = value; OnPropertyChanged(); }
        }
        public ICommand AppearingCommand { get; set; }
        public ICommand CompleteCommand { get; set; }
        public AppointmentsPageViewModel(IServiceProvider serviceProvider
            , RegionService regionService
            , ILogger<HomePageViewModel> logger
            )
        {
            this.ServiceProvider = serviceProvider;
            this.regionService = regionService;
            this.logger = logger;
            AppearingCommand = new DelegateCommand(OnAppearing);
            CompleteCommand = new DelegateCommand<AppointmentDetail>(OnComplete);
        }
        public async void OnComplete(AppointmentDetail detail)
        {
            var repo = new AppointmentDetails();
            await repo.Complete(detail);
            AppointmentDetails.Remove(detail);
        }
        public async void OnAppearing()
        {
            var repo = new AppointmentDetails();
            AppointmentDetails.Clear();
            foreach (var dtl in await repo.List())
            {
                AppointmentDetails.Add(dtl);
            }
        }
    }
}
