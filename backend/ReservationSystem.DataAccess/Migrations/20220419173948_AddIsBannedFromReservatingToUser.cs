using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservationSystem.DataAccess.Migrations
{
    public partial class AddIsBannedFromReservatingToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBannedFromReservating",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBannedFromReservating",
                table: "AspNetUsers");
        }
    }
}
