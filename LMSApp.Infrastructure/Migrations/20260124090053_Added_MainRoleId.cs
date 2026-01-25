using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMSApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Added_MainRoleId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MainRoleId",
                table: "AspNetUsers",
                type: "uuid",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1a44ae93-3974-4e67-baec-7dd27d9c47a9"),
                columns: new[] { "ConcurrencyStamp", "MainRoleId", "PasswordHash" },
                values: new object[] { "cae5b2b6-1793-43dc-9044-ec5aec7eeb6e", null, "AQAAAAIAAYagAAAAEI6AI7ZpidGkkHyUpKKyrrvhy0F7Jv1APnoU4Fg0s7MUoMIfzX7CtCITGt1nCIYJKw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainRoleId",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1a44ae93-3974-4e67-baec-7dd27d9c47a9"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8e2c2d4f-d919-4ce4-9940-6562fa14b1ca", "AQAAAAIAAYagAAAAEA5m/Z5ERdu/hErNN1Ym3zGedVIuF8W9TOByPIctsPz/0yPr9kPLxWSzqtEmLw8qIw==" });
        }
    }
}
