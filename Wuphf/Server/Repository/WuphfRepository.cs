using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Wuphf.Shared;

namespace Wuphf.Server.Repository
{


    public class WuphfRepository : DbContext
    {
        public DbSet<Account> Accounts { get; set; }

        public WuphfRepository(DbContextOptions<WuphfRepository> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.UserName);
            }); 
        }
    }

}


