using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Voluntio.Migrations
{
    /// <inheritdoc />
    public partial class modifUserEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserEventEntity_Event_EventId",
                table: "UserEventEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_UserEventEntity_User_UserId",
                table: "UserEventEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserEventEntity",
                table: "UserEventEntity");

            migrationBuilder.RenameTable(
                name: "UserEventEntity",
                newName: "UserEvents");

            migrationBuilder.RenameIndex(
                name: "IX_UserEventEntity_UserId",
                table: "UserEvents",
                newName: "IX_UserEvents_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserEvents",
                table: "UserEvents",
                columns: new[] { "EventId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserEvents_Event_EventId",
                table: "UserEvents",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserEvents_User_UserId",
                table: "UserEvents",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserEvents_Event_EventId",
                table: "UserEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_UserEvents_User_UserId",
                table: "UserEvents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserEvents",
                table: "UserEvents");

            migrationBuilder.RenameTable(
                name: "UserEvents",
                newName: "UserEventEntity");

            migrationBuilder.RenameIndex(
                name: "IX_UserEvents_UserId",
                table: "UserEventEntity",
                newName: "IX_UserEventEntity_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserEventEntity",
                table: "UserEventEntity",
                columns: new[] { "EventId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserEventEntity_Event_EventId",
                table: "UserEventEntity",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserEventEntity_User_UserId",
                table: "UserEventEntity",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
