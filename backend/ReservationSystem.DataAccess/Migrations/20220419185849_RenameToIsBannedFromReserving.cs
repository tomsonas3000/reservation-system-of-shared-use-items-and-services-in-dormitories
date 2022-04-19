using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservationSystem.DataAccess.Migrations
{
    public partial class RenameToIsBannedFromReserving : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsBannedFromReservating",
                table: "AspNetUsers",
                newName: "IsBannedFromReserving");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsBannedFromReserving",
                table: "AspNetUsers",
                newName: "IsBannedFromReservating");
        }
    }
}
