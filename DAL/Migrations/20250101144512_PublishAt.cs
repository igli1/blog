using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class PublishAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PublishAt",
                table: "Post",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("90c50356-919f-4270-9cb4-6b3ea5dc4c64"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEEuvsfgFCOlKAHBiTDDJZ6FMHHSj8T6Jdl7nYvz044Lk48+huvGNCpNS/eUa3DUM2w==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublishAt",
                table: "Post");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("90c50356-919f-4270-9cb4-6b3ea5dc4c64"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEF227uykXlBmgLpeHA4sKLnCQAdhrRpeiszqLFlhju+g5mICLbX4k1SBQ0958aehXg==");
        }
    }
}
