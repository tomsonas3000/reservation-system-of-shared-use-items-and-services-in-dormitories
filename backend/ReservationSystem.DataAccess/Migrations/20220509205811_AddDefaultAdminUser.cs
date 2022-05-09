using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservationSystem.DataAccess.Migrations
{
    public partial class AddDefaultAdminUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DormitoryId", "Email", "EmailConfirmed", "IsBannedFromReserving", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("d774af34-077a-4ec9-9a8f-0784a6e76dde"), 0, "ec36e97f-e732-41b6-8c67-c26342a37072", null, "admin@reservations.com", true, false, false, null, "Admin", "ADMIN@RESERVATIONS.com", "ADMIN", "AQAAAAEAACcQAAAAENshP56OgSK9rz+QrpS9Rpln69Nt0SH68B0rTU4xMsDRldpFStY1NjpprVjrsaFU/A==", "", false, "", "Admin", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("8d411aba-7e5e-4ff9-9aff-430b89beb461"), new Guid("d774af34-077a-4ec9-9a8f-0784a6e76dde") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("8d411aba-7e5e-4ff9-9aff-430b89beb461"), new Guid("d774af34-077a-4ec9-9a8f-0784a6e76dde") });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d774af34-077a-4ec9-9a8f-0784a6e76dde"));
        }
    }
}
