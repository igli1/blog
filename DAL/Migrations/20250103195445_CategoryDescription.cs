using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class CategoryDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Category",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("90c50356-919f-4270-9cb4-6b3ea5dc4c64"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEAbd7KzBrjF0uT+xMaO4H6M5ptF4hUURbWoqNsd7DIf8tU6jJ35AjuOuWwu3hV0ebw==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Category");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("90c50356-919f-4270-9cb4-6b3ea5dc4c64"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAENjChVpy6AlWZvblfVmVAfCMTayZEuQFsoGN8IDtJFXBPKKexF8LA+IyyWOfJtJQ9w==");
        }
    }
}
