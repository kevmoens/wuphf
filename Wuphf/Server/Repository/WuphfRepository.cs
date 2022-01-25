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
        //public string DbPath { get; }

        //public WuphfRepository()
        //{
        //    var folder = Environment.SpecialFolder.LocalApplicationData;
        //    var path = Environment.GetFolderPath(folder);
        //    DbPath = System.IO.Path.Join(path, "wuphf.db");
        //}

        //// The following configures EF to create a Sqlite database file in the
        //// special "local" folder for your platform.
        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //    => options.UseSqlite($"Data Source={DbPath}");
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.UserName);
            }); 
        }
    }

}


