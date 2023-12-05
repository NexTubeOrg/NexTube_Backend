using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NexTube.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddVideoAccessModificator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VideoAccessModificatorId",
                table: "Videos",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "VideoAccessModificators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Modificator = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoAccessModificators", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Videos_VideoAccessModificatorId",
                table: "Videos",
                column: "VideoAccessModificatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_VideoAccessModificators_VideoAccessModificatorId",
                table: "Videos",
                column: "VideoAccessModificatorId",
                principalTable: "VideoAccessModificators",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videos_VideoAccessModificators_VideoAccessModificatorId",
                table: "Videos");

            migrationBuilder.DropTable(
                name: "VideoAccessModificators");

            migrationBuilder.DropIndex(
                name: "IX_Videos_VideoAccessModificatorId",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "VideoAccessModificatorId",
                table: "Videos");
        }
    }
}
