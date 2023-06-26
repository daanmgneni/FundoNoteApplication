using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class Collab : Migration
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

            migrationBuilder.CreateTable(
                name: "Collaborator",
                columns: table => new
                {
                    CollaboratorID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollaboratedEmail = table.Column<string>(nullable: true),
                    NoteID = table.Column<long>(nullable: false),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collaborator", x => x.CollaboratorID);
                    table.ForeignKey(
                        name: "FK_Collaborator_Notes_NoteID",
                        column: x => x.NoteID,
                        principalTable: "Notes",
                        principalColumn: "NoteID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Collaborator_UserTable_UserId",
                        column: x => x.UserId,
                        principalTable: "UserTable",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Collaborator_NoteID",
                table: "Collaborator",
                column: "NoteID");

            migrationBuilder.CreateIndex(
                name: "IX_Collaborator_UserId",
                table: "Collaborator",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_UserTable_UserId",
                table: "Notes",
                column: "UserId",
                principalTable: "UserTable",
                principalColumn: "UserId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_UserTable_UserId",
                table: "Notes");

            migrationBuilder.DropTable(
                name: "Collaborator");

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
                onDelete: ReferentialAction.NoAction);
        }
    }
}
