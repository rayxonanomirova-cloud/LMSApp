using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMSApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class added_refreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrgId",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrgName",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1a44ae93-3974-4e67-baec-7dd27d9c47a9"),
                columns: new[] { "ConcurrencyStamp", "OrgId", "OrgName", "PasswordHash", "RefreshToken", "RefreshTokenExpiryTime" },
                values: new object[] { "8f032256-5a57-43be-9cf3-dad8415b42ea", null, null, "AQAAAAIAAYagAAAAEJ6G08zqqNBnFp2mZAheEXwDIDdgg0fdOu8sIrUR5McFDPAj8mjDAw7umEADJQslLQ==", null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrgId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "OrgName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1a44ae93-3974-4e67-baec-7dd27d9c47a9"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "cae5b2b6-1793-43dc-9044-ec5aec7eeb6e", "AQAAAAIAAYagAAAAEI6AI7ZpidGkkHyUpKKyrrvhy0F7Jv1APnoU4Fg0s7MUoMIfzX7CtCITGt1nCIYJKw==" });
        }
    }
}
