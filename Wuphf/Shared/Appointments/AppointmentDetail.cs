using System;
using System.ComponentModel.DataAnnotations;

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
        public DateTime CompletionDateTime { get; set; }
    }
}
