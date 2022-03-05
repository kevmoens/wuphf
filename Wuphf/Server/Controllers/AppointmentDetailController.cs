using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wuphf.Server.Repository;
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
    }
}
