using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DiplomaProject.Migrations
{
    /// <inheritdoc />
    public partial class Add_ExperimentGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VideoFileName",
                table: "VideoAnalyses");

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "VideoAnalyses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ExperimentGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SourceName = table.Column<string>(type: "text", nullable: false),
                    FramesDirectory = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperimentGroups", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VideoAnalyses_GroupId",
                table: "VideoAnalyses",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_VideoAnalyses_ExperimentGroups_GroupId",
                table: "VideoAnalyses",
                column: "GroupId",
                principalTable: "ExperimentGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VideoAnalyses_ExperimentGroups_GroupId",
                table: "VideoAnalyses");

            migrationBuilder.DropTable(
                name: "ExperimentGroups");

            migrationBuilder.DropIndex(
                name: "IX_VideoAnalyses_GroupId",
                table: "VideoAnalyses");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "VideoAnalyses");

            migrationBuilder.AddColumn<string>(
                name: "VideoFileName",
                table: "VideoAnalyses",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
