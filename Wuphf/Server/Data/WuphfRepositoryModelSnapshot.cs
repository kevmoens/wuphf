// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Wuphf.Server.Repository;

namespace Wuphf.Server.Data
{
    [DbContext(typeof(WuphfRepository))]
    partial class WuphfRepositoryModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.13");

            modelBuilder.Entity("Wuphf.Shared.Account", b =>
                {
                    b.Property<string>("UserName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.HasKey("UserName");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Wuphf.Shared.Appointments.Appointment", b =>
                {
                    b.Property<Guid>("AppointmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<int?>("NumDaysBetween")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Reoccurance")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("ScheduleTime")
                        .HasColumnType("TEXT");

                    b.Property<bool?>("SkipWeekend")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("TEXT");

                    b.Property<int?>("WeekDays")
                        .HasColumnType("INTEGER");

                    b.HasKey("AppointmentID");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("Wuphf.Shared.Appointments.AppointmentDetail", b =>
                {
                    b.Property<Guid>("DetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AppointmentId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("CompletionDateTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("SchedDateTime")
                        .HasColumnType("TEXT");

                    b.HasKey("DetailId");

                    b.ToTable("AppointmentDetails");
                });

            modelBuilder.Entity("Wuphf.Shared.Session.Session", b =>
                {
                    b.Property<string>("Token")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT");

                    b.HasKey("Token");

                    b.ToTable("Sessions");
                });
#pragma warning restore 612, 618
        }
    }
}
