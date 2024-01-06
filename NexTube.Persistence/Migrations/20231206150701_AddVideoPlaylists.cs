using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NexTube.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddVideoPlaylists : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlaylistId",
                table: "Videos",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "VideoPlaylists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatorId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoPlaylists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VideoPlaylists_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Videos_PlaylistId",
                table: "Videos",
                column: "PlaylistId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoPlaylists_CreatorId",
                table: "VideoPlaylists",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_VideoPlaylists_PlaylistId",
                table: "Videos",
                column: "PlaylistId",
                principalTable: "VideoPlaylists",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videos_VideoPlaylists_PlaylistId",
                table: "Videos");

            migrationBuilder.DropTable(
                name: "VideoPlaylists");

            migrationBuilder.DropIndex(
                name: "IX_Videos_PlaylistId",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "PlaylistId",
                table: "Videos");
        }
    }
}
