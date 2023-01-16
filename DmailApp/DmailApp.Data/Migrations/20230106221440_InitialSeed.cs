using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DmailApp.Data.Migrations
{
    public partial class InitialSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "HashedPassword", "Password", "user_type" },
                values: new object[,]
                {
                    { 1, "mate.matic@gmail.com", new byte[] { 136, 217, 45, 158, 96, 78, 60, 167, 225, 206, 223, 229, 10, 78, 173, 192, 203, 28, 177, 49, 158, 111, 116, 155, 96, 209, 9, 108, 215, 5, 193, 244, 240, 245, 23, 124, 73, 190, 255, 76, 34, 121, 203, 210, 195, 182, 213, 43, 71, 179, 28, 162, 117, 8, 118, 207, 237, 11, 56, 78, 47, 74, 249, 165 }, null, "user" },
                    { 2, "zeljana.zekic@gmail.com", new byte[] { 14, 88, 133, 150, 244, 225, 12, 160, 158, 151, 137, 51, 189, 193, 179, 152, 225, 245, 216, 8, 175, 133, 179, 187, 231, 29, 73, 84, 0, 218, 28, 209, 134, 58, 71, 41, 173, 43, 254, 120, 166, 159, 96, 146, 40, 18, 206, 93, 44, 63, 173, 225, 144, 123, 181, 191, 231, 63, 185, 114, 46, 110, 166, 133 }, null, "user" },
                    { 3, "anitamilic01@dump.com", new byte[] { 90, 14, 108, 6, 51, 31, 77, 17, 250, 54, 218, 244, 128, 62, 48, 222, 167, 169, 108, 141, 157, 230, 95, 10, 29, 161, 237, 162, 76, 54, 240, 187, 82, 122, 137, 51, 162, 18, 141, 217, 227, 165, 151, 154, 39, 150, 20, 181, 195, 186, 135, 149, 159, 36, 148, 189, 189, 194, 35, 38, 126, 198, 81, 80 }, null, "user" },
                    { 4, "ivo.ivic@gmail.com", new byte[] { 243, 229, 134, 23, 55, 7, 140, 0, 179, 203, 77, 192, 217, 176, 57, 240, 10, 158, 213, 112, 180, 148, 89, 194, 194, 77, 127, 210, 143, 89, 15, 36, 69, 69, 176, 95, 247, 50, 225, 99, 179, 44, 149, 182, 28, 97, 110, 96, 97, 196, 200, 207, 146, 146, 168, 35, 43, 66, 1, 22, 38, 48, 154, 85 }, null, "user" },
                    { 5, "marko.caric@dump.com", new byte[] { 25, 37, 165, 158, 84, 162, 113, 119, 119, 133, 234, 52, 93, 167, 246, 54, 170, 205, 136, 40, 105, 183, 119, 123, 98, 150, 235, 177, 124, 92, 82, 39, 43, 133, 136, 129, 182, 194, 183, 249, 208, 138, 199, 47, 47, 244, 190, 13, 24, 104, 107, 189, 162, 111, 159, 122, 230, 8, 51, 113, 18, 76, 63, 90 }, null, "user" },
                    { 6, "kate.bulj@gmail.com", new byte[] { 153, 107, 241, 175, 22, 229, 191, 104, 253, 123, 231, 87, 199, 12, 89, 160, 70, 204, 203, 194, 118, 159, 32, 110, 232, 106, 56, 21, 110, 139, 65, 117, 61, 111, 168, 185, 119, 91, 245, 123, 8, 234, 89, 199, 242, 252, 133, 163, 255, 199, 166, 152, 144, 220, 28, 159, 10, 32, 249, 33, 45, 6, 18, 186 }, null, "user" }
                });

            migrationBuilder.InsertData(
                table: "Mails",
                columns: new[] { "MailId", "Content", "EventDuration", "EventTime", "MailType", "SenderId", "TimeOfSending", "Title", "mail_type" },
                values: new object[,]
                {
                    { 1, "Write your review for your stay!", null, null, 0, 1, new DateTime(2022, 3, 13, 10, 0, 0, 0, DateTimeKind.Utc), "Airbnb", "mail" },
                    { 2, "Here are the notes i took today!", null, null, 0, 4, new DateTime(2022, 10, 2, 1, 11, 11, 0, DateTimeKind.Utc), "Notes from class", "mail" },
                    { 3, "Here are cheap flights i found:", null, null, 0, 6, new DateTime(2022, 9, 11, 9, 10, 0, 0, DateTimeKind.Utc), "Skyscanner", "mail" },
                    { 4, "Charge your phone before you leave!", null, null, 0, 3, new DateTime(2023, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Reminder", "mail" },
                    { 5, "Here are my most listened artists this year: ...", null, null, 0, 4, new DateTime(2022, 11, 29, 10, 0, 0, 0, DateTimeKind.Utc), "Spotify", "mail" },
                    { 6, null, new TimeSpan(1, 0, 0, 0, 0), new DateTime(2023, 1, 20, 15, 0, 0, 0, DateTimeKind.Utc), 1, 2, new DateTime(2022, 12, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Karaoke night", "mail" },
                    { 7, null, new TimeSpan(1, 0, 0, 0, 0), new DateTime(2023, 2, 20, 16, 0, 0, 0, DateTimeKind.Utc), 1, 5, new DateTime(2022, 12, 30, 10, 0, 0, 0, DateTimeKind.Utc), "Kate's Birthday", "mail" },
                    { 8, null, new TimeSpan(0, 2, 0, 0, 0), new DateTime(2023, 1, 16, 0, 0, 0, 0, DateTimeKind.Utc), 1, 2, new DateTime(2023, 1, 13, 10, 0, 0, 0, DateTimeKind.Utc), "Coffee date", "mail" },
                    { 9, null, new TimeSpan(4, 0, 0, 0, 0), new DateTime(2023, 7, 20, 0, 0, 0, 0, DateTimeKind.Utc), 1, 4, new DateTime(2022, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), "ConcertS in Italy", "mail" }
                });

            migrationBuilder.InsertData(
                table: "SpamFlag",
                columns: new[] { "SpamUserId", "UserId" },
                values: new object[,]
                {
                    { 2, 1 },
                    { 4, 1 },
                    { 1, 2 },
                    { 6, 3 },
                    { 1, 4 }
                });

            migrationBuilder.InsertData(
                table: "Recipients",
                columns: new[] { "MailId", "ReceiverId", "EventStatus", "MailStatus" },
                values: new object[,]
                {
                    { 6, 1, 1, null },
                    { 9, 1, 2, null },
                    { 1, 2, null, 0 },
                    { 8, 2, 0, null },
                    { 9, 2, 0, null },
                    { 3, 3, null, 0 },
                    { 4, 3, null, 0 },
                    { 7, 3, 2, null },
                    { 2, 4, null, 1 },
                    { 5, 6, null, 1 },
                    { 8, 6, 0, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Recipients",
                keyColumns: new[] { "MailId", "ReceiverId" },
                keyValues: new object[] { 6, 1 });

            migrationBuilder.DeleteData(
                table: "Recipients",
                keyColumns: new[] { "MailId", "ReceiverId" },
                keyValues: new object[] { 9, 1 });

            migrationBuilder.DeleteData(
                table: "Recipients",
                keyColumns: new[] { "MailId", "ReceiverId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "Recipients",
                keyColumns: new[] { "MailId", "ReceiverId" },
                keyValues: new object[] { 8, 2 });

            migrationBuilder.DeleteData(
                table: "Recipients",
                keyColumns: new[] { "MailId", "ReceiverId" },
                keyValues: new object[] { 9, 2 });

            migrationBuilder.DeleteData(
                table: "Recipients",
                keyColumns: new[] { "MailId", "ReceiverId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "Recipients",
                keyColumns: new[] { "MailId", "ReceiverId" },
                keyValues: new object[] { 4, 3 });

            migrationBuilder.DeleteData(
                table: "Recipients",
                keyColumns: new[] { "MailId", "ReceiverId" },
                keyValues: new object[] { 7, 3 });

            migrationBuilder.DeleteData(
                table: "Recipients",
                keyColumns: new[] { "MailId", "ReceiverId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "Recipients",
                keyColumns: new[] { "MailId", "ReceiverId" },
                keyValues: new object[] { 5, 6 });

            migrationBuilder.DeleteData(
                table: "Recipients",
                keyColumns: new[] { "MailId", "ReceiverId" },
                keyValues: new object[] { 8, 6 });

            migrationBuilder.DeleteData(
                table: "SpamFlag",
                keyColumns: new[] { "SpamUserId", "UserId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "SpamFlag",
                keyColumns: new[] { "SpamUserId", "UserId" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                table: "SpamFlag",
                keyColumns: new[] { "SpamUserId", "UserId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "SpamFlag",
                keyColumns: new[] { "SpamUserId", "UserId" },
                keyValues: new object[] { 6, 3 });

            migrationBuilder.DeleteData(
                table: "SpamFlag",
                keyColumns: new[] { "SpamUserId", "UserId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "Mails",
                keyColumn: "MailId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Mails",
                keyColumn: "MailId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Mails",
                keyColumn: "MailId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Mails",
                keyColumn: "MailId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Mails",
                keyColumn: "MailId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Mails",
                keyColumn: "MailId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Mails",
                keyColumn: "MailId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Mails",
                keyColumn: "MailId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Mails",
                keyColumn: "MailId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
