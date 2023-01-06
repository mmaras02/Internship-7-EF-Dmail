using Microsoft.EntityFrameworkCore;
using DmailApp.Data.Entities.Models;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography;
using DmailApp.Data.Entities.Enums;

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
                        HashedPassword = PasswordHashed("password",GenerateRandomSalt())
                    },
                    new User
                    {
                        Id = 2,
                        Email = "zeljana.zekic@gmail.com",
                        HashedPassword = PasswordHashed("123movie",GenerateRandomSalt())
                    },
                    new User
                    {
                        Id = 3,
                        Email = "anitamilic01@dump.com",
                        HashedPassword = PasswordHashed("london13",GenerateRandomSalt())
                    },
                    new User
                    {
                        Id = 4,
                        Email = "ivo.ivic@gmail.com",
                        HashedPassword = PasswordHashed("password",GenerateRandomSalt())
                    },
                    new User
                    {
                        Id = 5,
                        Email = "marko.caric@dump.com",
                        HashedPassword = PasswordHashed("password",GenerateRandomSalt())
                    },
                    new User
                    {
                        Id = 6,
                        Email = "kate.bulj@gmail.com",
                        HashedPassword = PasswordHashed("password",GenerateRandomSalt())
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
                        EventStatus=EventStatus.Rejected
                    },
                    new ReceiverMail()
                    {
                        MailId=7,
                        ReceiverId=3,
                        EventStatus=EventStatus.NoResponse
                    },
                    new ReceiverMail()
                    {
                        MailId=8,
                        ReceiverId=6,
                        EventStatus=EventStatus.Accepted
                    },
                    new ReceiverMail()
                    {
                        MailId=8,
                        ReceiverId=2,
                        EventStatus=EventStatus.Accepted
                    },
                    new ReceiverMail()
                    {
                        MailId=9,
                        ReceiverId=2,
                        EventStatus=EventStatus.Accepted
                    },
                    new ReceiverMail()
                    {
                        MailId=9,
                        ReceiverId=1,
                        EventStatus=EventStatus.NoResponse
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
        builder.Entity<Spam>()
           .HasData(new List<Spam>
                {
                    new Spam()
                    {
                        UserId=1,
                        SpamUserId=2
                    },
                    new Spam()
                    {
                        UserId=1,
                        SpamUserId=4
                    },
                    new Spam()
                    {
                        UserId=2,
                        SpamUserId=1
                    },
                    new Spam()
                    {
                        UserId=3,
                        SpamUserId=6
                    },
                    new Spam()
                    {
                        UserId=4,
                        SpamUserId=1
                    },

                });
    }
    static private byte[] PasswordHashed(string password, string salt)
    {
        var data = Encoding.UTF8.GetBytes(password + salt);
        var shaM = new SHA512Managed();
        return shaM.ComputeHash(data);
    }
    private static string GenerateRandomSalt()
    {
        int saltSize = 16;
        byte[] saltBytes = new byte[saltSize];
        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        rng.GetBytes(saltBytes);
        string salt = Convert.ToBase64String(saltBytes);
        return salt;
    }


}
