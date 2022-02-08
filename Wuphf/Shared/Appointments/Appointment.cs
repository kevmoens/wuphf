using System;
using System.ComponentModel.DataAnnotations;

namespace Wuphf.Shared.Appointments
{
    public class Appointment
    {
        public Appointment()
        {
            AppointmentID = Guid.NewGuid();
        }
        [Key]
        public Guid AppointmentID { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime? ScheduleTime { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        private ReoccuranceTypes? reoccuranceTypes = ReoccuranceTypes.None;
        [EnumDataType(typeof(ReoccuranceTypes))]
        public ReoccuranceTypes? Reoccurance { get => reoccuranceTypes; set { reoccuranceTypes = value; } }
        public int? NumDaysBetween { get; set; }
        public int? WeekDays { get; set; } //Su, Mo, Tu, We, Th, Fr, Sa (Bitwise)
        public bool? SkipWeekend { get; set; }

    }
}

