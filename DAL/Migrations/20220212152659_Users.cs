using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class Users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Logs",
                table: "Logs");

            migrationBuilder.RenameTable(
                name: "Logs",
                newName: "MailMeUpUserLogs");

            migrationBuilder.AddColumn<int>(
                name: "MailMeUpUserId",
                table: "MailMeUpUserLogs",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MailMeUpUserLogs",
                table: "MailMeUpUserLogs",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "MailMeUpUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    IsAdmin = table.Column<bool>(type: "INTEGER", nullable: false),
                    EmailUsername = table.Column<string>(type: "TEXT", nullable: false),
                    EmailPassword = table.Column<string>(type: "TEXT", nullable: false),
                    EmailAddress = table.Column<string>(type: "TEXT", nullable: false),
                    ActiveToken = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailMeUpUsers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MailMeUpUserLogs_MailMeUpUserId",
                table: "MailMeUpUserLogs",
                column: "MailMeUpUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MailMeUpUserLogs_MailMeUpUsers_MailMeUpUserId",
                table: "MailMeUpUserLogs",
                column: "MailMeUpUserId",
                principalTable: "MailMeUpUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MailMeUpUserLogs_MailMeUpUsers_MailMeUpUserId",
                table: "MailMeUpUserLogs");

            migrationBuilder.DropTable(
                name: "MailMeUpUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MailMeUpUserLogs",
                table: "MailMeUpUserLogs");

            migrationBuilder.DropIndex(
                name: "IX_MailMeUpUserLogs_MailMeUpUserId",
                table: "MailMeUpUserLogs");

            migrationBuilder.DropColumn(
                name: "MailMeUpUserId",
                table: "MailMeUpUserLogs");

            migrationBuilder.RenameTable(
                name: "MailMeUpUserLogs",
                newName: "Logs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Logs",
                table: "Logs",
                column: "Id");
        }
    }
}
