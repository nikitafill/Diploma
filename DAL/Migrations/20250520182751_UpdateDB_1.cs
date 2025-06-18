using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DiplomaProject.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDB_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VideoAnalyses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VideoFileName = table.Column<string>(type: "text", nullable: false),
                    ExtractedFramePath = table.Column<string>(type: "text", nullable: false),
                    PixelsPerCm = table.Column<float>(type: "real", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoAnalyses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RingsRadiuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VideoAnalysisId = table.Column<int>(type: "integer", nullable: false),
                    RadiusCm = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RingsRadiuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RingsRadiuses_VideoAnalyses_VideoAnalysisId",
                        column: x => x.VideoAnalysisId,
                        principalTable: "VideoAnalyses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RingsRadiuses_VideoAnalysisId",
                table: "RingsRadiuses",
                column: "VideoAnalysisId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RingsRadiuses");

            migrationBuilder.DropTable(
                name: "VideoAnalyses");
        }
    }
}
