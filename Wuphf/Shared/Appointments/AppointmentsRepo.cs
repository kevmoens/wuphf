using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Wuphf.Shared.Appointments
{
    public class AppointmentsRepo : INotifyPropertyChanged, IAppointmentsRepo
    {
        public AppointmentsRepo(HttpClient http)
        {
            this.http = http;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private HttpClient http;
        private bool hasSelectedAppointment;
        public bool HasSelectedAppointment
        {
            get { return hasSelectedAppointment; }
            set
            {
                hasSelectedAppointment = value;
                OnPropertyChanged();
            }
        }
        private Appointment selectedAppointment;
        public Appointment SelectedAppointment
        {
            get { return selectedAppointment; }
            set
            {
                selectedAppointment = value;
                dayOfWeekAppointment.Appointment = value;
                OnPropertyChanged();
            }
        }
        private DayOfWeekAppointment dayOfWeekAppointment = new DayOfWeekAppointment();
        public DayOfWeekAppointment DayOfWeekAppointment
        {
            get { return dayOfWeekAppointment;}
            set {
                dayOfWeekAppointment = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Appointment> appointments = new ObservableCollection<Appointment>();
        public ObservableCollection<Appointment> Appointments
        {
            get { return appointments; }
            set
            {
                appointments = value;
                OnPropertyChanged();
            }
        }

        public async Task GetAppointments()
        {
            var result = await http.GetFromJsonAsync<Appointment[]>("Appointment");
            if (result == null)
            {
                Appointments = new ObservableCollection<Appointment>();
                return;
            }
            var apts = result?.ToList();
            if (apts == null)
            {
                Appointments = new ObservableCollection<Appointment>();
            }
            Appointments = new ObservableCollection<Appointment>(apts);
        }
        public void OnAdd()
        {
            if (appointments == null)
            {
                appointments = new ObservableCollection<Appointment>();
            }
            SelectedAppointment = new Appointment();
            hasSelectedAppointment = true;
            SelectedAppointment.StartDate = DateTime.Today;
            appointments.Add(SelectedAppointment);
        }
        public void OnEdit(Appointment appt)
        {
            SelectedAppointment = appt;
            hasSelectedAppointment = true;
        }
        public async Task OnRemove(Appointment appt)
        {
            await http.DeleteAsync($"Appointment/{appt.AppointmentID}");
            appointments.Remove(appt);
            if (appt == selectedAppointment)
            {
                selectedAppointment = null;
                hasSelectedAppointment = false;
            }
        }

        public async Task SaveSelectedAppointment()
        {
            //Add or Update
            await http.PostAsJsonAsync<Appointment>("Appointment", selectedAppointment);
        }
    }
}

