using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class LabelMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollabTable_NoteTable_NoteId",
                table: "CollabTable");

            migrationBuilder.DropForeignKey(
                name: "FK_CollabTable_UserTable_UserId",
                table: "CollabTable");

            migrationBuilder.DropIndex(
                name: "IX_CollabTable_NoteId",
                table: "CollabTable");

            migrationBuilder.DropIndex(
                name: "IX_CollabTable_UserId",
                table: "CollabTable");

            migrationBuilder.CreateTable(
                name: "LabelTable",
                columns: table => new
                {
                    LabelId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LabelName = table.Column<string>(nullable: true),
                    UserId = table.Column<long>(nullable: false),
                    NoteId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabelTable", x => x.LabelId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LabelTable");

            migrationBuilder.CreateIndex(
                name: "IX_CollabTable_NoteId",
                table: "CollabTable",
                column: "NoteId");

            migrationBuilder.CreateIndex(
                name: "IX_CollabTable_UserId",
                table: "CollabTable",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CollabTable_NoteTable_NoteId",
                table: "CollabTable",
                column: "NoteId",
                principalTable: "NoteTable",
                principalColumn: "NoteId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CollabTable_UserTable_UserId",
                table: "CollabTable",
                column: "UserId",
                principalTable: "UserTable",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
