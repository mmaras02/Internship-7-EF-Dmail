using DmailApp.Data.Entities.Enums;
using DmailApp.Data.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace DmailApp.Data.Seeds;

public class DatabaseSeeder
{
    public static void Seed(ModelBuilder builder)
    {
        builder.Entity<User>()
               .HasData(new List<User>
               {
                    new User
                    {
                        Id = 1,
                        Email = "mate.matic@gmail.com",
                        HashedPassword = PasswordHashed("password")
                    },
                    new User
                    {
                        Id = 2,
                        Email = "zeljana.zekic@gmail.com",
                        HashedPassword = PasswordHashed("123movie")
                    },
                    new User
                    {
                        Id = 3,
                        Email = "anitamilic01@dump.com",
                        HashedPassword = PasswordHashed("london13")
                    },
                    new User
                    {
                        Id = 4,
                        Email = "ivo.ivic@gmail.com",
                        HashedPassword = PasswordHashed("heloo12")
                    },
                    new User
                    {
                        Id = 5,
                        Email = "marko.caric@dump.com",
                        HashedPassword = PasswordHashed("password")
                    },
                    new User
                    {
                        Id = 6,
                        Email = "kate.bulj@gmail.com",
                        HashedPassword = PasswordHashed("password")
                    }
               });
        builder.Entity<Mail>()
            .HasData(new List<Mail>
                {
                    new Mail("Airbnb")
                    {
                        MailId = 1,
                        SenderId=1,
                        MailType=MailType.MessageMail,
                        TimeOfSending = new DateTime(2022, 03, 13, 10, 0, 0, DateTimeKind.Utc),
                        Content="Write your review for your stay!"
                    },
                    new Mail("Notes from class")
                    {
                        MailId = 2,
                        SenderId=4,
                        MailType=MailType.MessageMail,
                        TimeOfSending = new DateTime(2022, 10, 02,01,11,11, DateTimeKind.Utc),
                        Content="Here are the notes i took today!"
                    },
                    new Mail("Skyscanner")
                    {
                        MailId = 3,
                        SenderId=6,
                        MailType=MailType.MessageMail,
                        TimeOfSending = new DateTime(2022, 09, 11, 09, 10, 0, DateTimeKind.Utc),
                        Content="Here are cheap flights i found:"
                    },
                    new Mail("Reminder")
                    {
                        MailId = 4,
                        SenderId=3,
                        MailType=MailType.MessageMail,
                        TimeOfSending = new DateTime(2023, 01, 05, 0, 0, 0, DateTimeKind.Utc),
                        Content="Charge your phone before you leave!"
                    },
                    new Mail("Spotify")
                    {
                        MailId = 5,
                        SenderId=4,
                        MailType=MailType.MessageMail,
                        TimeOfSending = new DateTime(2022, 11, 29, 10, 0, 0, DateTimeKind.Utc),
                        Content="Here are my most listened artists this year: ..."
                    },
                    new Mail("Karaoke night")
                    {
                        MailId = 6,
                        SenderId=2,
                        MailType=MailType.EventMail,
                        TimeOfSending = new DateTime(2022, 12, 18, 0, 0, 0, DateTimeKind.Utc),
                        EventTime = new DateTime(2023, 01, 20, 15, 0, 0, DateTimeKind.Utc),
                        EventDuration= TimeSpan.FromDays(1)
                    },
                    new Mail("Kate's Birthday")
                    {
                        MailId = 7,
                        SenderId=5,
                        MailType=MailType.EventMail,
                        TimeOfSending = new DateTime(2022, 12, 30, 10, 0, 0, DateTimeKind.Utc),
                        EventTime = new DateTime(2023, 02, 20, 16, 0, 0, DateTimeKind.Utc),
                        EventDuration= TimeSpan.FromDays(1)
                    },
                    new Mail("Coffee date")
                    {
                        MailId = 8,
                        SenderId=2,
                        MailType=MailType.EventMail,
                        TimeOfSending = new DateTime(2023, 01, 13, 10, 0, 0, DateTimeKind.Utc),
                        EventTime = new DateTime(2023, 01, 16, 0, 0, 0, DateTimeKind.Utc),
                        EventDuration= TimeSpan.FromMinutes(120)
                    },
                    new Mail("ConcertS in Italy")
                    {
                        MailId = 9,
                        SenderId=4,
                        MailType=MailType.EventMail,
                        TimeOfSending = new DateTime(2022, 10, 15, 0, 0, 0, DateTimeKind.Utc),
                        EventTime = new DateTime(2023, 07, 20, 0, 0, 0, DateTimeKind.Utc),
                        EventDuration= TimeSpan.FromDays(4)
                    },
                });
        builder.Entity<ReceiverMail>()
           .HasData(new List<ReceiverMail>
                {
                    new ReceiverMail()
                    {
                        MailId=3,
                        ReceiverId=3,
                        MailStatus=MailStatus.Read
                    },
                    new ReceiverMail()
                    {
                        MailId=2,
                        ReceiverId=4,
                        MailStatus=MailStatus.Unread
                    },
                    new ReceiverMail()
                    {
                        MailId=1,
                        ReceiverId=2,
                        MailStatus=MailStatus.Read

                    },
                    new ReceiverMail()
                    {
                        MailId=6,
                        ReceiverId=1,
                        EventStatus=EventStatus.Rejected,
                        MailStatus=MailStatus.Read
                    },
                    new ReceiverMail()
                    {
                        MailId=7,
                        ReceiverId=3,
                        EventStatus=EventStatus.NoResponse,
                        MailStatus=MailStatus.Unread

                    },
                    new ReceiverMail()
                    {
                        MailId=8,
                        ReceiverId=6,
                        EventStatus=EventStatus.Accepted,
                        MailStatus=MailStatus.Read
                    },
                    new ReceiverMail()
                    {
                        MailId=8,
                        ReceiverId=2,
                        EventStatus=EventStatus.Accepted,
                        MailStatus=MailStatus.Read
                    },
                    new ReceiverMail()
                    {
                        MailId=9,
                        ReceiverId=2,
                        EventStatus=EventStatus.Accepted,
                        MailStatus=MailStatus.Read
                    },
                    new ReceiverMail()
                    {
                        MailId=9,
                        ReceiverId=1,
                        EventStatus=EventStatus.NoResponse,
                        MailStatus=MailStatus.Unread
                    },
                    new ReceiverMail()
                    {
                        MailId=4,
                        ReceiverId=3,
                        MailStatus=MailStatus.Read
                    },
                    new ReceiverMail()
                    {
                        MailId=5,
                        ReceiverId=6,
                        MailStatus=MailStatus.Unread
                    }
                });
        builder.Entity<SpamFlag>()
           .HasData(new List<SpamFlag>
                {
                    new SpamFlag()
                    {
                        UserId=1,
                        SpamUserId=2
                    },
                    new SpamFlag()
                    {
                        UserId=1,
                        SpamUserId=4
                    },
                    new SpamFlag()
                    {
                        UserId=2,
                        SpamUserId=1
                    },
                    new SpamFlag()
                    {
                        UserId=3,
                        SpamUserId=6
                    },
                    new SpamFlag()
                    {
                        UserId=4,
                        SpamUserId=1
                    },

                });
    }
    static private byte[] PasswordHashed(string password)
    {
        var data = Encoding.UTF8.GetBytes(password);
        var shaM = new SHA512Managed();
        return shaM.ComputeHash(data);
    }

}
