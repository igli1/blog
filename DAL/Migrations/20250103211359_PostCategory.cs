using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class PostCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostCategory_Category_CategoryId",
                table: "PostCategory");

            migrationBuilder.DropIndex(
                name: "IX_PostCategory_CategoryId",
                table: "PostCategory");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "PostCategory");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("90c50356-919f-4270-9cb4-6b3ea5dc4c64"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEJVNsLZp9VyHJagFcdUh5V/7lFCAygjM7lkOqNHzGJIFDGWJP15N1SU3TOPNNtIHGA==");

            migrationBuilder.CreateIndex(
                name: "IX_PostCategory_CategoryGuid",
                table: "PostCategory",
                column: "CategoryGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_PostCategory_Category_CategoryGuid",
                table: "PostCategory",
                column: "CategoryGuid",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostCategory_Category_CategoryGuid",
                table: "PostCategory");

            migrationBuilder.DropIndex(
                name: "IX_PostCategory_CategoryGuid",
                table: "PostCategory");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "PostCategory",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("90c50356-919f-4270-9cb4-6b3ea5dc4c64"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEAbd7KzBrjF0uT+xMaO4H6M5ptF4hUURbWoqNsd7DIf8tU6jJ35AjuOuWwu3hV0ebw==");

            migrationBuilder.CreateIndex(
                name: "IX_PostCategory_CategoryId",
                table: "PostCategory",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostCategory_Category_CategoryId",
                table: "PostCategory",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
