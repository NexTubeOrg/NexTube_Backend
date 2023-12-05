using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexTube.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RenameSomeFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videos_VideoAccessModificators_VideoAccessModificatorId",
                table: "Videos");

            migrationBuilder.RenameColumn(
                name: "VideoId",
                table: "Videos",
                newName: "VideoFileId");

            migrationBuilder.RenameColumn(
                name: "VideoAccessModificatorId",
                table: "Videos",
                newName: "AccessModificatorId");

            migrationBuilder.RenameColumn(
                name: "PreviewPhotoId",
                table: "Videos",
                newName: "PreviewPhotoFileId");

            migrationBuilder.RenameIndex(
                name: "IX_Videos_VideoAccessModificatorId",
                table: "Videos",
                newName: "IX_Videos_AccessModificatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_VideoAccessModificators_AccessModificatorId",
                table: "Videos",
                column: "AccessModificatorId",
                principalTable: "VideoAccessModificators",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videos_VideoAccessModificators_AccessModificatorId",
                table: "Videos");

            migrationBuilder.RenameColumn(
                name: "VideoFileId",
                table: "Videos",
                newName: "VideoId");

            migrationBuilder.RenameColumn(
                name: "PreviewPhotoFileId",
                table: "Videos",
                newName: "PreviewPhotoId");

            migrationBuilder.RenameColumn(
                name: "AccessModificatorId",
                table: "Videos",
                newName: "VideoAccessModificatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Videos_AccessModificatorId",
                table: "Videos",
                newName: "IX_Videos_VideoAccessModificatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_VideoAccessModificators_VideoAccessModificatorId",
                table: "Videos",
                column: "VideoAccessModificatorId",
                principalTable: "VideoAccessModificators",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
