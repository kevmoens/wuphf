using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wuphf.Server.Repository;
using Wuphf.Shared;
using Wuphf.Shared.Appointments;

namespace Wuphf.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppointmentDetailController : Controller
    {
        WuphfRepository repository;
        public AppointmentDetailController(WuphfRepository repository)
        {
            this.repository = repository;

            //repository.Database.EnsureCreated();
            repository.Database.Migrate();
        }
        // GET: values
        [HttpGet]
        public IEnumerable<AppointmentDetail> Get()
        {
            return repository.AppointmentDetails.ToList();
        }

        // GET: to do
        [HttpGet("{dateTime}")]
        public IEnumerable<AppointmentDetail> Get(DateTime dateTime)
        {
            var details = repository.AppointmentDetails.AsEnumerable().Where(d => d.SchedDateTime <= dateTime && d.CompletionDateTime == null).ToList();
            foreach (var dtl in details)
            {
                dtl.Appointment = repository.Appointments.FirstOrDefault(a => a.AppointmentID == dtl.AppointmentId);                
            }
            return details.OrderBy((d) => { return d.SchedDateTime; });
        }

        // POST values (Add)
        [HttpPost]
        public void Post(AppointmentDetail value)
        {
            var result = repository.AppointmentDetails.AsEnumerable().FirstOrDefault((a) => a.DetailId == value.DetailId);
            if (result == null)
            {
                //Add
                repository.AppointmentDetails.Add(value);

                repository.SaveChanges();
                return;
            }
            //Update
            result.CompletionDateTime = value.CompletionDateTime;

            repository.AppointmentDetails.Update(result);
            repository.SaveChanges();
        }

        [HttpPost("Delay")]
        public void Delay(AppointmentDetail value)
        {
            var result = repository.AppointmentDetails.AsEnumerable().FirstOrDefault((a) => a.DetailId == value.DetailId);
            if (result == null)
            {
                return;
            }
            var appt = repository.Appointments.AsEnumerable().FirstOrDefault((a) => a.AppointmentID == value.AppointmentId);
            if (appt == null)
            {
                return;
            }
            //Update
            result.SchedDateTime = UpdateDateTime(appt, result.SchedDateTime); 
            repository.AppointmentDetails.Update(result);

            if (appt.Reoccurance != Shared.ReoccuranceTypes.None)
            {
                foreach (var detail in repository
                    .AppointmentDetails
                    .Where(item =>
                        item.AppointmentId == value.AppointmentId
                        && item.CompletionDateTime == null
                        && item.SchedDateTime > result.SchedDateTime)
                    )
                {
                    detail.SchedDateTime = UpdateDateTime(appt, detail.SchedDateTime);
                    repository.AppointmentDetails.Update(detail);
                }
            }

            repository.SaveChanges();
        }
        public DateTime UpdateDateTime(Appointment appt, DateTime schedDateTime)
        {
            DateTime newDateTime = schedDateTime;
            switch (appt.Reoccurance)
            {
                case Shared.ReoccuranceTypes.Daily:
                    newDateTime = newDateTime.AddDays(1);
                    if (appt.SkipWeekend.GetValueOrDefault())
                    {
                        switch (newDateTime.DayOfWeek)
                        {
                            case DayOfWeek.Saturday:
                                newDateTime.AddDays(2);
                                break;
                            case DayOfWeek.Sunday:
                                newDateTime.AddDays(1);
                                break;
                        }
                    }
                    break;
                case Shared.ReoccuranceTypes.Weekly:
                    do
                    {
                        newDateTime = newDateTime.AddDays(1);
                    } while (!((appt.WeekDays & (int)newDateTime.DayOfWeek.ToBitwise()) == (int)newDateTime.DayOfWeek.ToBitwise()));
                    break;
                default:
                    newDateTime.AddDays(1);
                    return schedDateTime;
            }
            return newDateTime;       
        }
    }
}
