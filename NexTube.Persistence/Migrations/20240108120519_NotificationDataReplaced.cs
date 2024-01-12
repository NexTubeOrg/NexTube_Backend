using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexTube.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NotificationDataReplaced : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotificationData",
                table: "Notifications");

            migrationBuilder.AddColumn<int>(
                name: "NotificationDataId",
                table: "Notifications",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_NotificationDataId",
                table: "Notifications",
                column: "NotificationDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Videos_NotificationDataId",
                table: "Notifications",
                column: "NotificationDataId",
                principalTable: "Videos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Videos_NotificationDataId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_NotificationDataId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "NotificationDataId",
                table: "Notifications");

            migrationBuilder.AddColumn<string>(
                name: "NotificationData",
                table: "Notifications",
                type: "jsonb",
                nullable: true);
        }
    }
}
