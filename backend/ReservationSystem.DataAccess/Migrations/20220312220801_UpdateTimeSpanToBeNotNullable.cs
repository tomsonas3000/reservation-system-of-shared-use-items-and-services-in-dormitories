using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservationSystem.DataAccess.Migrations
{
    public partial class UpdateTimeSpanToBeNotNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "MaxTimeOfUse",
                table: "Services",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0),
                oldClrType: typeof(TimeSpan),
                oldType: "time",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "MaxTimeOfUse",
                table: "Services",
                type: "time",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time");
        }
    }
}
