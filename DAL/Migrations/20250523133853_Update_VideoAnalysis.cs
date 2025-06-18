using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiplomaProject.Migrations
{
    /// <inheritdoc />
    public partial class Update_VideoAnalysis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "CentreX",
                table: "VideoAnalyses",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "CentreY",
                table: "VideoAnalyses",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CentreX",
                table: "VideoAnalyses");

            migrationBuilder.DropColumn(
                name: "CentreY",
                table: "VideoAnalyses");
        }
    }
}
