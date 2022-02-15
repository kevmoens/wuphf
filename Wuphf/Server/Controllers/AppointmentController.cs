using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wuphf.Server.Repository;
using Wuphf.Shared;
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
                AddAppointment(value);

                repository.SaveChanges();
                return;
            }
            UpdateAppointment(value, result);
            repository.SaveChanges();
        }
        private void AddAppointment(Appointment value)
        {
            //Add
            repository.Appointments.Add(value);

            AppointmentDetailBuilder builder = new AppointmentDetailBuilder();
            builder.Appointment = value;

            foreach (var dtl in builder.GetDetails())
            {
                repository.AppointmentDetails.Add(dtl);
            }
        }
        private void UpdateAppointment(Appointment updReq, Appointment existing)
        {

            UpdateAppointmentRecord(updReq, existing);

            bool recordExistsForToday = UpdateAppointmentRemoveUnfinishedDtls(updReq);

            UpdateAppointmentCreateNewDetails(updReq, recordExistsForToday);

        }
        private void UpdateAppointmentRecord(Appointment updReq, Appointment existing)
        {

            //Update
            existing.Description = updReq.Description;
            existing.EndDate = updReq.EndDate;
            existing.NumDaysBetween = updReq.NumDaysBetween;
            existing.Reoccurance = updReq.Reoccurance;
            existing.ScheduleTime = updReq.ScheduleTime;
            existing.SkipWeekend = updReq.SkipWeekend;
            existing.WeekDays = updReq.WeekDays;
            repository.Appointments.Update(existing);
        }
        private bool UpdateAppointmentRemoveUnfinishedDtls(Appointment updReq)
        {
            var remApptDtls = repository
                .AppointmentDetails
                .Where((a) =>
                    a.AppointmentId == updReq.AppointmentID
                    && a.CompletionDateTime == null
                );
            bool recordExistsForToday = false;
            foreach (var dtl in remApptDtls)
            {
                repository.AppointmentDetails.Remove(dtl);
                if (dtl.SchedDateTime.Date == DateTime.Today)
                {
                    recordExistsForToday = true;
                }
            }
            return recordExistsForToday;
        }
        private void UpdateAppointmentCreateNewDetails(Appointment updReq, bool recordExistsForToday)
        {

            updReq.StartDate = DateTime.Today;
            if (recordExistsForToday)
            {
                updReq.StartDate = updReq.StartDate.Value.AddDays(1);
            }

            AppointmentDetailBuilder updBuilder = new AppointmentDetailBuilder();
            updBuilder.Appointment = updReq;

            foreach (var dtl in updBuilder.GetDetails())
            {
                repository.AppointmentDetails.Add(dtl);
            }
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
            var remApptDtls = repository.AppointmentDetails.Where((a) => a.AppointmentId == apptId);
            foreach (var dtl in remApptDtls)
            {
                repository.AppointmentDetails.Remove(dtl);
            }
            repository.SaveChanges();
        }
    }
}
