using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DiplomaProject.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserQuestionAnswermodels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAnswers_Experiments_ExperimentId",
                table: "StudentAnswers");

            migrationBuilder.DropTable(
                name: "ExperimentParameters");

            migrationBuilder.DropTable(
                name: "ExperimentResults");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "Experiments");

            migrationBuilder.DropIndex(
                name: "IX_StudentAnswers_ExperimentId",
                table: "StudentAnswers");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ExperimentGroups",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExperimentGroups_UserId",
                table: "ExperimentGroups",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExperimentGroups_Users_UserId",
                table: "ExperimentGroups",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExperimentGroups_Users_UserId",
                table: "ExperimentGroups");

            migrationBuilder.DropIndex(
                name: "IX_ExperimentGroups_UserId",
                table: "ExperimentGroups");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ExperimentGroups");

            migrationBuilder.CreateTable(
                name: "Experiments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ExperimentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Experiments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExperimentParameters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ExperimentId = table.Column<int>(type: "integer", nullable: false),
                    Pressure = table.Column<double>(type: "double precision", nullable: false),
                    Radius = table.Column<double>(type: "double precision", nullable: false),
                    RefractiveIndex = table.Column<double>(type: "double precision", nullable: false),
                    Wavelength = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperimentParameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExperimentParameters_Experiments_ExperimentId",
                        column: x => x.ExperimentId,
                        principalTable: "Experiments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExperimentResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ExperimentId = table.Column<int>(type: "integer", nullable: false),
                    CalculationData = table.Column<string>(type: "text", nullable: true),
                    MaxRingCount = table.Column<int>(type: "integer", nullable: false),
                    RingPatternImage = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperimentResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExperimentResults_Experiments_ExperimentId",
                        column: x => x.ExperimentId,
                        principalTable: "Experiments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ExperimentId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ReportText = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reports_Experiments_ExperimentId",
                        column: x => x.ExperimentId,
                        principalTable: "Experiments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentAnswers_ExperimentId",
                table: "StudentAnswers",
                column: "ExperimentId");

            migrationBuilder.CreateIndex(
                name: "IX_ExperimentParameters_ExperimentId",
                table: "ExperimentParameters",
                column: "ExperimentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExperimentResults_ExperimentId",
                table: "ExperimentResults",
                column: "ExperimentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Experiments_UserId",
                table: "Experiments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_ExperimentId",
                table: "Reports",
                column: "ExperimentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAnswers_Experiments_ExperimentId",
                table: "StudentAnswers",
                column: "ExperimentId",
                principalTable: "Experiments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
