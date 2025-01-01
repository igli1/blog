using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class Refactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Note_AspNetUsers_UserId",
                table: "Note");

            migrationBuilder.DropForeignKey(
                name: "FK_NoteCategory_Category_CategoryId",
                table: "NoteCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_NoteCategory_Note_PostGuid",
                table: "NoteCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NoteCategory",
                table: "NoteCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Note",
                table: "Note");

            migrationBuilder.RenameTable(
                name: "NoteCategory",
                newName: "PostCategory");

            migrationBuilder.RenameTable(
                name: "Note",
                newName: "Post");

            migrationBuilder.RenameIndex(
                name: "IX_NoteCategory_CategoryId",
                table: "PostCategory",
                newName: "IX_PostCategory_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Note_UserId",
                table: "Post",
                newName: "IX_Post_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostCategory",
                table: "PostCategory",
                columns: new[] { "PostGuid", "CategoryGuid" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Post",
                table: "Post",
                column: "Guid");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("90c50356-919f-4270-9cb4-6b3ea5dc4c64"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAENnG7Grv4T3VHWRwDhBF5FFFZvdLoXP8VnX1uferWYrQgn2j1GqVJjc2ArCghxRMXg==");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_AspNetUsers_UserId",
                table: "Post",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostCategory_Category_CategoryId",
                table: "PostCategory",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostCategory_Post_PostGuid",
                table: "PostCategory",
                column: "PostGuid",
                principalTable: "Post",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_AspNetUsers_UserId",
                table: "Post");

            migrationBuilder.DropForeignKey(
                name: "FK_PostCategory_Category_CategoryId",
                table: "PostCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_PostCategory_Post_PostGuid",
                table: "PostCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostCategory",
                table: "PostCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Post",
                table: "Post");

            migrationBuilder.RenameTable(
                name: "PostCategory",
                newName: "NoteCategory");

            migrationBuilder.RenameTable(
                name: "Post",
                newName: "Note");

            migrationBuilder.RenameIndex(
                name: "IX_PostCategory_CategoryId",
                table: "NoteCategory",
                newName: "IX_NoteCategory_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Post_UserId",
                table: "Note",
                newName: "IX_Note_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NoteCategory",
                table: "NoteCategory",
                columns: new[] { "PostGuid", "CategoryGuid" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Note",
                table: "Note",
                column: "Guid");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("90c50356-919f-4270-9cb4-6b3ea5dc4c64"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEI/mS9cek/2UACVkq9GgCC4ePHbLJsvl73JHttABjOO69QOrqjtNP1ZXu7sAOYYQUg==");

            migrationBuilder.AddForeignKey(
                name: "FK_Note_AspNetUsers_UserId",
                table: "Note",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NoteCategory_Category_CategoryId",
                table: "NoteCategory",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NoteCategory_Note_PostGuid",
                table: "NoteCategory",
                column: "PostGuid",
                principalTable: "Note",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
