using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexTube.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCommentOwner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "VideoComments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_VideoComments_OwnerId",
                table: "VideoComments",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_VideoComments_AspNetUsers_OwnerId",
                table: "VideoComments",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VideoComments_AspNetUsers_OwnerId",
                table: "VideoComments");

            migrationBuilder.DropIndex(
                name: "IX_VideoComments_OwnerId",
                table: "VideoComments");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "VideoComments");
        }
    }
}
