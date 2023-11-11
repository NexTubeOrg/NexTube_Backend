using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexTube.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class VideosAddDatesOfCreateEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateOfUpload",
                table: "Videos",
                newName: "DateModified");

            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "Videos",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Videos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Videos_CreatorId",
                table: "Videos",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_AspNetUsers_CreatorId",
                table: "Videos",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videos_AspNetUsers_CreatorId",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Videos_CreatorId",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Videos");

            migrationBuilder.RenameColumn(
                name: "DateModified",
                table: "Videos",
                newName: "DateOfUpload");
        }
    }
}
