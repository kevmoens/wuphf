using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wuphf.Server.Repository;
using Wuphf.Shared;

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
    }
}
