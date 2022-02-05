using System;
using Wuphf.Shared;

namespace Wuphf.Client.Models
{
    public class DayOfWeekAppointment
    {
        private Appointment appointment;
        public Appointment Appointment { get { return appointment; } set { appointment = value; UpdateBitwise(); } }
        private bool sunday;
        public bool Sunday { get { return sunday; } set { sunday = value; UpdateAppointment(); } }
        private bool monday;
        public bool Monday { get { return monday; } set { monday = value; UpdateAppointment(); } }
        private bool tuesday;
        public bool Tuesday { get { return tuesday; } set { tuesday = value; UpdateAppointment(); } }
        private bool wednesday;
        public bool Wednesday { get { return wednesday; } set { wednesday = value; UpdateAppointment(); } }
        private bool thursday;
        public bool Thursday { get { return thursday; } set { thursday = value; UpdateAppointment(); } }
        private bool friday;
        public bool Friday { get { return friday; } set { friday = value; UpdateAppointment(); } }
        private bool saturday;
        public bool Saturday { get { return saturday; } set { saturday = value; UpdateAppointment(); } }
        private void UpdateAppointment()
        {
            Appointment.WeekDays = (sunday ? (int)DayOfWeekBitwise.Sunday : 0)
                + (monday ? (int)DayOfWeekBitwise.Monday : 0)
                + (tuesday ? (int)DayOfWeekBitwise.Tuesday : 0)
                + (wednesday ? (int)DayOfWeekBitwise.Wednesday : 0)
                + (thursday ? (int)DayOfWeekBitwise.Thursday : 0)
                + (friday ? (int)DayOfWeekBitwise.Friday : 0)
                + (saturday ? (int)DayOfWeekBitwise.Saturday : 0);
        }
        private void UpdateBitwise()
        {
            DayOfWeekBitwise days = DayOfWeekBitwise.None;
            if (appointment.WeekDays == null)
            {
                return;
            }
            days = (DayOfWeekBitwise)appointment.WeekDays;
            sunday = days.HasFlag(DayOfWeekBitwise.Sunday);
            monday = days.HasFlag(DayOfWeekBitwise.Monday);
            tuesday = days.HasFlag(DayOfWeekBitwise.Tuesday);
            wednesday = days.HasFlag(DayOfWeekBitwise.Wednesday);
            thursday = days.HasFlag(DayOfWeekBitwise.Thursday);
            friday = days.HasFlag(DayOfWeekBitwise.Friday);
            saturday = days.HasFlag(DayOfWeekBitwise.Saturday);
        }
    }
}
