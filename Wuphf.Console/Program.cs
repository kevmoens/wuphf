using System;
using Wuphf.Shared;
using Wuphf.Shared.Appointments;

namespace Wuphf.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Appointment appt = new Appointment();
            appt.Description = "Bedtime";
            appt.StartDate = DateTime.Parse("2022/01/31");
            appt.ScheduleTime = DateTime.Parse("08:10 PM");
            appt.EndDate = DateTime.Parse("2022/02/18");
            appt.Reoccurance = Shared.ReoccuranceTypes.Weekly;
            //Weekly
            appt.WeekDays = (int)(DayOfWeekBitwise.Monday | DayOfWeekBitwise.Tuesday | DayOfWeekBitwise.Wednesday | DayOfWeekBitwise.Thursday | DayOfWeekBitwise.Sunday);
            //Daily
            //appt.NumDaysBetween = 0;
            //appt.SkipWeekend = true;

            AppointmentDetailBuilder builder = new AppointmentDetailBuilder();
            builder.Appointment = appt;           

            foreach (var dtl in builder.GetDetails())
            {
                System.Console.WriteLine(dtl.SchedDateTime.ToShortDateString() + " " + dtl.SchedDateTime.ToShortTimeString());
            }
            System.Console.ReadLine();
        }
    }
}
