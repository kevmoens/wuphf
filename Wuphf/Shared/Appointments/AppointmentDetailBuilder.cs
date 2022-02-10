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
            switch (Appointment.Reoccurance)
            {
                case ReoccuranceTypes.None:
                    return details;
                case ReoccuranceTypes.Daily:
                    return GetDailyDetails(initDtl, details);
                case ReoccuranceTypes.Weekly:
                    return GetWeeklyDetails(initDtl, details);
            }

            return details;
        }
        private AppointmentDetail GetInitialDetail()
        {
            AppointmentDetail dtl = new AppointmentDetail();
            dtl.SchedDateTime = DateTime.Parse(Appointment.StartDate.Value.ToShortDateString() + " " + Appointment.ScheduleTime.Value.ToShortTimeString());
            dtl.AppointmentId = Appointment.AppointmentID;
            return dtl;
        }
        private IEnumerable<AppointmentDetail> GetWeeklyDetails(AppointmentDetail initDtl, List<AppointmentDetail> details)
        {
            if (Appointment.WeekDays == null)
            {
                return details;
            }
            var startDate = Appointment.StartDate.Value;
            var endDate = Appointment.EndDate.Value;
            var spanDays = endDate.Subtract(startDate);
            var currDate = startDate;
            for (int i = 1; i < (uint)spanDays.TotalDays; i++)
            {
                currDate = startDate.AddDays(i);
                
                if ((Appointment.WeekDays & (int)currDate.DayOfWeek.ToBitwise()) == (int)currDate.DayOfWeek.ToBitwise())
                {
                    AppointmentDetail dtl = new AppointmentDetail();
                    dtl.SchedDateTime = DateTime.Parse(currDate.ToShortDateString() + " " + Appointment.ScheduleTime.Value.ToShortTimeString());
                    dtl.AppointmentId = Appointment.AppointmentID;
                    details.Add(dtl);
                }
            }
            return details;
        }

        private IEnumerable<AppointmentDetail> GetDailyDetails(AppointmentDetail initDtl, List<AppointmentDetail> details)
        {
            int step;
            if (Appointment.NumDaysBetween == null || Appointment.NumDaysBetween == 0)
            {
                step = 1;
            } else
            {
                step = Appointment.NumDaysBetween.Value;
            }

            var startDate = Appointment.StartDate.Value;
            var endDate = Appointment.EndDate.Value;
            var currDate = startDate;
            bool firstTime = true;
            do
            {
               if (!firstTime)
                {
                    AppointmentDetail dtl = new AppointmentDetail();
                    dtl.SchedDateTime = DateTime.Parse(currDate.ToShortDateString() + " " + Appointment.ScheduleTime.Value.ToShortTimeString());
                    dtl.AppointmentId = Appointment.AppointmentID;
                    details.Add(dtl);
                }

                firstTime = false;
                currDate.AddDays(step);
                if (Appointment.SkipWeekend.GetValueOrDefault())
                {
                    switch (currDate.DayOfWeek)
                    {
                        case DayOfWeek.Saturday:
                            currDate.AddDays(2);
                            break;
                        case DayOfWeek.Sunday:
                            currDate.AddDays(1);
                            break;
                    }
                }

            } while (currDate <= endDate);

            return details;
        }

    }
}
