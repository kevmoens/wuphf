using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wuphf.Server.Data
{
    public partial class AddAppointments3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    AppointmentID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    ScheduleTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Reoccurance = table.Column<int>(type: "INTEGER", nullable: false),
                    NumDaysBetween = table.Column<int>(type: "INTEGER", nullable: true),
                    WeekDays = table.Column<int>(type: "INTEGER", nullable: true),
                    SkipWeekend = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.AppointmentID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");
        }
    }
}
