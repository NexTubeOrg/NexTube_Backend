using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexTube.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class VideoMayBeInMultiplePlaylists : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videos_VideoPlaylists_PlaylistId",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Videos_PlaylistId",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "PlaylistId",
                table: "Videos");

            migrationBuilder.CreateTable(
                name: "PlaylistsVideosManyToMany",
                columns: table => new
                {
                    VideoId = table.Column<int>(type: "integer", nullable: false),
                    PlaylistId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaylistsVideosManyToMany", x => new { x.VideoId, x.PlaylistId });
                    table.ForeignKey(
                        name: "FK_PlaylistsVideosManyToMany_VideoPlaylists_PlaylistId",
                        column: x => x.PlaylistId,
                        principalTable: "VideoPlaylists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaylistsVideosManyToMany_Videos_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Videos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistsVideosManyToMany_PlaylistId",
                table: "PlaylistsVideosManyToMany",
                column: "PlaylistId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlaylistsVideosManyToMany");

            migrationBuilder.AddColumn<int>(
                name: "PlaylistId",
                table: "Videos",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Videos_PlaylistId",
                table: "Videos",
                column: "PlaylistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_VideoPlaylists_PlaylistId",
                table: "Videos",
                column: "PlaylistId",
                principalTable: "VideoPlaylists",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
