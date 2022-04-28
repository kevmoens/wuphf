using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Wuphf.Shared;
using Wuphf.Shared.Appointments;
using Wuphf.Shared.Session;

namespace Wuphf.Server.Repository
{
    public interface IWuphfRepository 
    {
        DbSet<Account> Accounts { get; set; }
        DbSet<Session> Sessions { get; set; }
        DbSet<Appointment> Appointments { get; set; }
        DbSet<AppointmentDetail> AppointmentDetails { get; set; }
        DatabaseFacade Database { get; }
        int SaveChanges();
    }
}