using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexTube.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddTargetVideoToReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VideoId",
                table: "Reports",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reports_VideoId",
                table: "Reports",
                column: "VideoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Videos_VideoId",
                table: "Reports",
                column: "VideoId",
                principalTable: "Videos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Videos_VideoId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_VideoId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "VideoId",
                table: "Reports");
        }
    }
}
