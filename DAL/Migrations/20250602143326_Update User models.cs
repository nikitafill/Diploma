using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiplomaProject.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUsermodels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Users",
                newName: "Login");

            migrationBuilder.AddColumn<string>(
                name: "GroupName",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupName",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Login",
                table: "Users",
                newName: "Email");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
