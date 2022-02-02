using System;
using System.ComponentModel.DataAnnotations;

namespace Wuphf.Shared
{
    public class Appointment
    {
        public Appointment()
        {
        }
        [Key]
        public Guid AppointmentID { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime? ScheduleTime { get; set; }
        public DateTime? EndDate { get; set; }
        private ReoccuranceTypes? reoccuranceTypes = ReoccuranceTypes.None;
        [EnumDataType(typeof(ReoccuranceTypes))]
        public ReoccuranceTypes? Reoccurance { get => reoccuranceTypes; set { reoccuranceTypes = value; } }
        public int? NumDaysBetween { get; set; }
        public int? WeekDays { get; set; }
        public bool? SkipWeekend { get; set; }

    }
}

