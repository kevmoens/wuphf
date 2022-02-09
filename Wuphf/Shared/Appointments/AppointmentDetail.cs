using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wuphf.Shared.Appointments
{
    public class AppointmentDetail
    {
        public AppointmentDetail()
        {
        }

        [Key]
        public Guid AppointmentId { get; set; }
        [Key]
        public Guid DetailId { get; set; }

        public DateTime SchedDateTime { get; set; }
        public DateTime? CompletionDateTime { get; set; }

        [NotMapped]
        public Appointment? Appointment { get; set; }
    }
}
