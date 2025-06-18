using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiplomaProject.Migrations
{
    /// <inheritdoc />
    public partial class UpdateExperimentGroupModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExperimentGroups_Users_UserId",
                table: "ExperimentGroups");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "ExperimentGroups",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ExperimentGroups_Users_UserId",
                table: "ExperimentGroups",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExperimentGroups_Users_UserId",
                table: "ExperimentGroups");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "ExperimentGroups",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_ExperimentGroups_Users_UserId",
                table: "ExperimentGroups",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
