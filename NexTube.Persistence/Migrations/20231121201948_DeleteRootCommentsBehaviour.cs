using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexTube.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DeleteRootCommentsBehaviour : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VideoComments_VideoComments_RepliedToId",
                table: "VideoComments");

            migrationBuilder.AddForeignKey(
                name: "FK_VideoComments_VideoComments_RepliedToId",
                table: "VideoComments",
                column: "RepliedToId",
                principalTable: "VideoComments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VideoComments_VideoComments_RepliedToId",
                table: "VideoComments");

            migrationBuilder.AddForeignKey(
                name: "FK_VideoComments_VideoComments_RepliedToId",
                table: "VideoComments",
                column: "RepliedToId",
                principalTable: "VideoComments",
                principalColumn: "Id");
        }
    }
}
