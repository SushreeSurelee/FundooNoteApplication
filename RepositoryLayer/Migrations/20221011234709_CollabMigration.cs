using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class CollabMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NoteTable_UserTable_UserId",
                table: "NoteTable");

            migrationBuilder.DropIndex(
                name: "IX_NoteTable_UserId",
                table: "NoteTable");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "NoteTable",
                newName: "UserID");

            migrationBuilder.CreateTable(
                name: "CollabTable",
                columns: table => new
                {
                    CollabId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollabEmail = table.Column<string>(nullable: true),
                    Edited = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    NoteId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollabTable", x => x.CollabId);
                    table.ForeignKey(
                        name: "FK_CollabTable_NoteTable_NoteId",
                        column: x => x.NoteId,
                        principalTable: "NoteTable",
                        principalColumn: "NoteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollabTable_UserTable_UserId",
                        column: x => x.UserId,
                        principalTable: "UserTable",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollabTable_NoteId",
                table: "CollabTable",
                column: "NoteId");

            migrationBuilder.CreateIndex(
                name: "IX_CollabTable_UserId",
                table: "CollabTable",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollabTable");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "NoteTable",
                newName: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_NoteTable_UserId",
                table: "NoteTable",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_NoteTable_UserTable_UserId",
                table: "NoteTable",
                column: "UserId",
                principalTable: "UserTable",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
