using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DmailApp.Data.Migrations
{
    public partial class DatabaseUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Recipients",
                keyColumns: new[] { "MailId", "ReceiverId" },
                keyValues: new object[] { 9, 1 },
                column: "MailStatus",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Recipients",
                keyColumns: new[] { "MailId", "ReceiverId" },
                keyValues: new object[] { 7, 3 },
                column: "MailStatus",
                value: 1);
            migrationBuilder.UpdateData(
                table: "Recipients",
                keyColumns: new[] { "MailId", "ReceiverId" },
                keyValues: new object[] { 9, 2 },
                column: "MailStatus",
                value: 1);
            migrationBuilder.UpdateData(
                table: "Recipients",
                keyColumns: new[] { "MailId", "ReceiverId" },
                keyValues: new object[] { 6, 1 },
                column: "MailStatus",
                value: 1);
            migrationBuilder.UpdateData(
                table: "Recipients",
                keyColumns: new[] { "MailId", "ReceiverId" },
                keyValues: new object[] { 8, 6 },
                column: "MailStatus",
                value: 1);
            migrationBuilder.UpdateData(
                table: "Recipients",
                keyColumns: new[] { "MailId", "ReceiverId" },
                keyValues: new object[] { 8, 2 },
                column: "MailStatus",
                value: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Recipients",
                keyColumns: new[] { "MailId", "ReceiverId" },
                keyValues: new object[] { 9, 1 },
                column: "MailStatus",
                value: null);

            migrationBuilder.UpdateData(
                table: "Recipients",
                keyColumns: new[] { "MailId", "ReceiverId" },
                keyValues: new object[] { 7, 3 },
                column: "MailStatus",
                value: null);

            migrationBuilder.UpdateData(
                table: "Recipients",
                keyColumns: new[] { "MailId", "ReceiverId" },
                keyValues: new object[] { 9, 2 },
                column: "MailStatus",
                value: null);

            migrationBuilder.UpdateData(
                table: "Recipients",
                keyColumns: new[] { "MailId", "ReceiverId" },
                keyValues: new object[] { 6, 1 },
                column: "MailStatus",
                value: null);

            migrationBuilder.UpdateData(
                table: "Recipients",
                keyColumns: new[] { "MailId", "ReceiverId" },
                keyValues: new object[] { 8, 6 },
                column: "MailStatus",
                value: null);

            migrationBuilder.UpdateData(
                table: "Recipients",
                keyColumns: new[] { "MailId", "ReceiverId" },
                keyValues: new object[] { 8, 2 },
                column: "MailStatus",
                value: null);
        }
    }
}
