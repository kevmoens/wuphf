using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Wuphf.Shared;
using Wuphf.Shared.Session;

namespace Wuphf.Server.Repository
{


    public class WuphfRepository : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentDetail> AppointmentDetails { get; set; }

        public WuphfRepository(DbContextOptions<WuphfRepository> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.UserName);
            });
            modelBuilder.Entity<Session>(entity =>
            {
                entity.HasKey(e => e.Token);
            });
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasKey(e => e.AppointmentID);
            });
            modelBuilder.Entity<AppointmentDetail>(entity =>
            {
                entity.HasKey(e => e.DetailId);
            });
        }
    }

}


