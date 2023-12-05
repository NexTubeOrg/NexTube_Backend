using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NexTube.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class VideoReactionEntity_UseCompositePrimaryKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_VideoReactions",
                table: "VideoReactions");

            migrationBuilder.DropIndex(
                name: "IX_VideoReactions_CreatorId",
                table: "VideoReactions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "VideoReactions");

            migrationBuilder.AlterColumn<int>(
                name: "CreatorId",
                table: "VideoReactions",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VideoReactions",
                table: "VideoReactions",
                columns: new[] { "CreatorId", "ReactedVideoId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_VideoReactions",
                table: "VideoReactions");

            migrationBuilder.AlterColumn<int>(
                name: "CreatorId",
                table: "VideoReactions",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "VideoReactions",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VideoReactions",
                table: "VideoReactions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_VideoReactions_CreatorId",
                table: "VideoReactions",
                column: "CreatorId");
        }
    }
}
