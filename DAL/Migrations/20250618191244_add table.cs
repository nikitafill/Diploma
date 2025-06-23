using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DiplomaProject.Migrations
{
    /// <inheritdoc />
    public partial class addtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExperimentId",
                table: "StudentAnswers",
                newName: "ExperimentGroupId");

            migrationBuilder.CreateTable(
                name: "ExperimentGroupQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ExperimentGroupId = table.Column<int>(type: "integer", nullable: false),
                    QuestionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperimentGroupQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExperimentGroupQuestions_ExperimentGroups_ExperimentGroupId",
                        column: x => x.ExperimentGroupId,
                        principalTable: "ExperimentGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExperimentGroupQuestions_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentAnswers_ExperimentGroupId",
                table: "StudentAnswers",
                column: "ExperimentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ExperimentGroupQuestions_ExperimentGroupId",
                table: "ExperimentGroupQuestions",
                column: "ExperimentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ExperimentGroupQuestions_QuestionId",
                table: "ExperimentGroupQuestions",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAnswers_ExperimentGroups_ExperimentGroupId",
                table: "StudentAnswers",
                column: "ExperimentGroupId",
                principalTable: "ExperimentGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAnswers_ExperimentGroups_ExperimentGroupId",
                table: "StudentAnswers");

            migrationBuilder.DropTable(
                name: "ExperimentGroupQuestions");

            migrationBuilder.DropIndex(
                name: "IX_StudentAnswers_ExperimentGroupId",
                table: "StudentAnswers");

            migrationBuilder.RenameColumn(
                name: "ExperimentGroupId",
                table: "StudentAnswers",
                newName: "ExperimentId");
        }
    }
}
