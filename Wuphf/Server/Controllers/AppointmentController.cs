using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wuphf.Server.Repository;
using Wuphf.Shared.Appointments;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Wuphf.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppointmentController : Controller
    {
        WuphfRepository repository;
        public AppointmentController(WuphfRepository repository)
        {
            this.repository = repository;

            //repository.Database.EnsureCreated();
            repository.Database.Migrate();
        }
        // GET: values
        [HttpGet]
        public IEnumerable<Appointment> Get()
        {
            return repository.Appointments.ToList();
        }

        // POST values (Add)
        [HttpPost]
        public void Post(Appointment value)
        {
            var result = repository.Appointments.AsEnumerable().FirstOrDefault((a) => a.AppointmentID == value.AppointmentID);
            if (result == null)
            {
                //Add
                repository.Appointments.Add(value);
                
                repository.SaveChanges();
                return;
            }
            //Update
            result.Description = value.Description;
            result.EndDate = value.EndDate;
            result.NumDaysBetween = value.NumDaysBetween;
            result.Reoccurance = value.Reoccurance;
            result.ScheduleTime = value.ScheduleTime;
            result.SkipWeekend = value.SkipWeekend;
            result.WeekDays = value.WeekDays;

            repository.Appointments.Update(result);
            repository.SaveChanges();
        }


        // DELETE appointment/{guid}
        [HttpDelete("{apptId}")]
        public void Delete(Guid apptId)
        {
            var remAppts = repository.Appointments.Where((a) => a.AppointmentID == apptId);
            foreach (var appt in remAppts)
            {
                repository.Appointments.Remove(appt);
            }
            repository.SaveChanges();
        }
    }
}
