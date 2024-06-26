using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyForum.Model.Migrations.ReptileDb
{
    /// <inheritdoc />
    public partial class _121 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "SchoolNotifications",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Lectures",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Link",
                table: "Lectures",
                newName: "link");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Lectures",
                newName: "date");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Books",
                newName: "title");

            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "SchoolNotifications",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "date",
                table: "Lectures",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "title",
                table: "SchoolNotifications",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "Lectures",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "link",
                table: "Lectures",
                newName: "Link");

            migrationBuilder.RenameColumn(
                name: "date",
                table: "Lectures",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "Books",
                newName: "Title");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "SchoolNotifications",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Lectures",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
