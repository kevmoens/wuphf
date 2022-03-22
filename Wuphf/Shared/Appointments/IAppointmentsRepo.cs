using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Wuphf.Shared.Appointments
{
    public interface IAppointmentsRepo
    {

        event PropertyChangedEventHandler PropertyChanged;

        Appointment SelectedAppointment { get; set; }
        ObservableCollection<Appointment> Appointments { get; set; }
        bool HasSelectedAppointment { get; set; }
        DayOfWeekAppointment DayOfWeekAppointment { get; set; }

        Task GetAppointments();
        void OnAdd();
        void OnEdit(Appointment appt);
        Task OnRemove(Appointment appt);
        Task SaveSelectedAppointment();
    }
}