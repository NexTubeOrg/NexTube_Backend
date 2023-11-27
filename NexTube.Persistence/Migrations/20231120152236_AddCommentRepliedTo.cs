using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexTube.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCommentRepliedTo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RepliedToId",
                table: "VideoComments",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VideoComments_RepliedToId",
                table: "VideoComments",
                column: "RepliedToId");

            migrationBuilder.AddForeignKey(
                name: "FK_VideoComments_VideoComments_RepliedToId",
                table: "VideoComments",
                column: "RepliedToId",
                principalTable: "VideoComments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VideoComments_VideoComments_RepliedToId",
                table: "VideoComments");

            migrationBuilder.DropIndex(
                name: "IX_VideoComments_RepliedToId",
                table: "VideoComments");

            migrationBuilder.DropColumn(
                name: "RepliedToId",
                table: "VideoComments");
        }
    }
}
