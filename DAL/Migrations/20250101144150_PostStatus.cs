using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class PostStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Post",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("90c50356-919f-4270-9cb4-6b3ea5dc4c64"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEF227uykXlBmgLpeHA4sKLnCQAdhrRpeiszqLFlhju+g5mICLbX4k1SBQ0958aehXg==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Post");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("90c50356-919f-4270-9cb4-6b3ea5dc4c64"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAENnG7Grv4T3VHWRwDhBF5FFFZvdLoXP8VnX1uferWYrQgn2j1GqVJjc2ArCghxRMXg==");
        }
    }
}
