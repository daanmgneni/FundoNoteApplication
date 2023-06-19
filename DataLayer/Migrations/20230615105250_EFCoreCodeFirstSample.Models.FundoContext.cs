using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class EFCoreCodeFirstSampleModelsFundoContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_UserTable_userid",
                table: "Notes");

            migrationBuilder.RenameColumn(
                name: "userid",
                table: "Notes",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Notes_userid",
                table: "Notes",
                newName: "IX_Notes_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_UserTable_UserId",
                table: "Notes",
                column: "UserId",
                principalTable: "UserTable",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_UserTable_UserId",
                table: "Notes");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Notes",
                newName: "userid");

            migrationBuilder.RenameIndex(
                name: "IX_Notes_UserId",
                table: "Notes",
                newName: "IX_Notes_userid");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_UserTable_userid",
                table: "Notes",
                column: "userid",
                principalTable: "UserTable",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
