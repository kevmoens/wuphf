using System;
using System.Collections.Generic;

namespace Wuphf.Shared.Appointments
{
    public class AppointmentDetailBuilder
    {
        public AppointmentDetailBuilder()
        {
        }

        public Appointment Appointment { get; set; }
        public IEnumerable<AppointmentDetail> GetDetails()
        {
            List<AppointmentDetail> details = new List<AppointmentDetail>();
            var initDtl = GetInitialDetail();
            details.Add(initDtl);

            return details;
        }
        private AppointmentDetail GetInitialDetail()
        {
            AppointmentDetail dtl = new AppointmentDetail();
            dtl.SchedDateTime = DateTime.Parse(Appointment.StartDate.Value.ToShortDateString() + " " + Appointment.ScheduleTime.Value.ToShortTimeString());
            dtl.AppointmentId = Appointment.AppointmentID;
            return dtl;
        }
    }
}
