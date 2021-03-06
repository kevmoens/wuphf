// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Wuphf.Server.Repository;

namespace Wuphf.Server.Data
{
    [DbContext(typeof(WuphfRepository))]
    [Migration("20220130233132_AddAppointmentTable2")]
    partial class AddAppointmentTable2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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
