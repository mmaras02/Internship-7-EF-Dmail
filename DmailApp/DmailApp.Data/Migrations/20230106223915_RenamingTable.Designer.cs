// <auto-generated />
using System;
using DmailApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DmailApp.Data.Migrations
{
    [DbContext(typeof(DmailAppDbContext))]
    [Migration("20230106223915_RenamingTable")]
    partial class RenamingTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DmailApp.Data.Entities.Models.Mail", b =>
                {
                    b.Property<int>("MailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("MailId"));

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<TimeSpan?>("EventDuration")
                        .HasColumnType("interval");

                    b.Property<DateTime?>("EventTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("MailType")
                        .HasColumnType("integer");

                    b.Property<int>("SenderId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("TimeOfSending")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("mail_type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("MailId");

                    b.HasIndex("SenderId");

                    b.ToTable("Mails");

                    b.HasDiscriminator<string>("mail_type").HasValue("mail");

                    b.HasData(
                        new
                        {
                            MailId = 1,
                            Content = "Write your review for your stay!",
                            MailType = 0,
                            SenderId = 1,
                            TimeOfSending = new DateTime(2022, 3, 13, 10, 0, 0, 0, DateTimeKind.Utc),
                            Title = "Airbnb"
                        },
                        new
                        {
                            MailId = 2,
                            Content = "Here are the notes i took today!",
                            MailType = 0,
                            SenderId = 4,
                            TimeOfSending = new DateTime(2022, 10, 2, 1, 11, 11, 0, DateTimeKind.Utc),
                            Title = "Notes from class"
                        },
                        new
                        {
                            MailId = 3,
                            Content = "Here are cheap flights i found:",
                            MailType = 0,
                            SenderId = 6,
                            TimeOfSending = new DateTime(2022, 9, 11, 9, 10, 0, 0, DateTimeKind.Utc),
                            Title = "Skyscanner"
                        },
                        new
                        {
                            MailId = 4,
                            Content = "Charge your phone before you leave!",
                            MailType = 0,
                            SenderId = 3,
                            TimeOfSending = new DateTime(2023, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc),
                            Title = "Reminder"
                        },
                        new
                        {
                            MailId = 5,
                            Content = "Here are my most listened artists this year: ...",
                            MailType = 0,
                            SenderId = 4,
                            TimeOfSending = new DateTime(2022, 11, 29, 10, 0, 0, 0, DateTimeKind.Utc),
                            Title = "Spotify"
                        },
                        new
                        {
                            MailId = 6,
                            EventDuration = new TimeSpan(1, 0, 0, 0, 0),
                            EventTime = new DateTime(2023, 1, 20, 15, 0, 0, 0, DateTimeKind.Utc),
                            MailType = 1,
                            SenderId = 2,
                            TimeOfSending = new DateTime(2022, 12, 18, 0, 0, 0, 0, DateTimeKind.Utc),
                            Title = "Karaoke night"
                        },
                        new
                        {
                            MailId = 7,
                            EventDuration = new TimeSpan(1, 0, 0, 0, 0),
                            EventTime = new DateTime(2023, 2, 20, 16, 0, 0, 0, DateTimeKind.Utc),
                            MailType = 1,
                            SenderId = 5,
                            TimeOfSending = new DateTime(2022, 12, 30, 10, 0, 0, 0, DateTimeKind.Utc),
                            Title = "Kate's Birthday"
                        },
                        new
                        {
                            MailId = 8,
                            EventDuration = new TimeSpan(0, 2, 0, 0, 0),
                            EventTime = new DateTime(2023, 1, 16, 0, 0, 0, 0, DateTimeKind.Utc),
                            MailType = 1,
                            SenderId = 2,
                            TimeOfSending = new DateTime(2023, 1, 13, 10, 0, 0, 0, DateTimeKind.Utc),
                            Title = "Coffee date"
                        },
                        new
                        {
                            MailId = 9,
                            EventDuration = new TimeSpan(4, 0, 0, 0, 0),
                            EventTime = new DateTime(2023, 7, 20, 0, 0, 0, 0, DateTimeKind.Utc),
                            MailType = 1,
                            SenderId = 4,
                            TimeOfSending = new DateTime(2022, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc),
                            Title = "ConcertS in Italy"
                        });
                });

            modelBuilder.Entity("DmailApp.Data.Entities.Models.ReceiverMail", b =>
                {
                    b.Property<int>("ReceiverId")
                        .HasColumnType("integer");

                    b.Property<int>("MailId")
                        .HasColumnType("integer");

                    b.Property<int?>("EventStatus")
                        .HasColumnType("integer");

                    b.Property<int?>("MailStatus")
                        .HasColumnType("integer");

                    b.HasKey("ReceiverId", "MailId");

                    b.HasIndex("MailId");

                    b.ToTable("Recipients");

                    b.HasData(
                        new
                        {
                            ReceiverId = 3,
                            MailId = 3,
                            MailStatus = 0
                        },
                        new
                        {
                            ReceiverId = 4,
                            MailId = 2,
                            MailStatus = 1
                        },
                        new
                        {
                            ReceiverId = 2,
                            MailId = 1,
                            MailStatus = 0
                        },
                        new
                        {
                            ReceiverId = 1,
                            MailId = 6,
                            EventStatus = 1
                        },
                        new
                        {
                            ReceiverId = 3,
                            MailId = 7,
                            EventStatus = 2
                        },
                        new
                        {
                            ReceiverId = 6,
                            MailId = 8,
                            EventStatus = 0
                        },
                        new
                        {
                            ReceiverId = 2,
                            MailId = 8,
                            EventStatus = 0
                        },
                        new
                        {
                            ReceiverId = 2,
                            MailId = 9,
                            EventStatus = 0
                        },
                        new
                        {
                            ReceiverId = 1,
                            MailId = 9,
                            EventStatus = 2
                        },
                        new
                        {
                            ReceiverId = 3,
                            MailId = 4,
                            MailStatus = 0
                        },
                        new
                        {
                            ReceiverId = 6,
                            MailId = 5,
                            MailStatus = 1
                        });
                });

            modelBuilder.Entity("DmailApp.Data.Entities.Models.Spam", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("SpamUserId")
                        .HasColumnType("integer");

                    b.HasKey("UserId", "SpamUserId");

                    b.HasIndex("SpamUserId");

                    b.ToTable("SpamFlag");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            SpamUserId = 2
                        },
                        new
                        {
                            UserId = 1,
                            SpamUserId = 4
                        },
                        new
                        {
                            UserId = 2,
                            SpamUserId = 1
                        },
                        new
                        {
                            UserId = 3,
                            SpamUserId = 6
                        },
                        new
                        {
                            UserId = 4,
                            SpamUserId = 1
                        });
                });

            modelBuilder.Entity("DmailApp.Data.Entities.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("user_type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("user_type").HasValue("user");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "mate.matic@gmail.com",
                            HashedPassword = new byte[] { 128, 149, 138, 162, 70, 89, 221, 238, 236, 128, 145, 188, 52, 247, 50, 14, 174, 235, 220, 35, 203, 130, 0, 250, 20, 254, 146, 132, 218, 178, 5, 219, 190, 147, 204, 144, 109, 222, 233, 52, 33, 73, 7, 228, 28, 141, 183, 202, 53, 58, 245, 34, 118, 88, 4, 43, 212, 116, 99, 106, 111, 21, 5, 7 }
                        },
                        new
                        {
                            Id = 2,
                            Email = "zeljana.zekic@gmail.com",
                            HashedPassword = new byte[] { 73, 200, 224, 21, 237, 185, 232, 139, 36, 1, 184, 138, 188, 72, 95, 109, 62, 196, 173, 14, 248, 59, 117, 108, 54, 96, 233, 12, 50, 103, 222, 202, 232, 248, 227, 54, 140, 174, 6, 96, 254, 24, 72, 241, 83, 60, 49, 42, 32, 33, 203, 34, 9, 81, 40, 69, 100, 159, 86, 208, 151, 1, 64, 77 }
                        },
                        new
                        {
                            Id = 3,
                            Email = "anitamilic01@dump.com",
                            HashedPassword = new byte[] { 116, 15, 236, 208, 167, 156, 247, 191, 82, 105, 129, 201, 233, 196, 193, 167, 208, 120, 57, 246, 106, 47, 77, 241, 207, 94, 184, 27, 252, 151, 205, 136, 93, 14, 130, 209, 1, 119, 107, 153, 110, 193, 226, 172, 173, 87, 4, 28, 109, 22, 228, 150, 39, 94, 167, 103, 224, 226, 7, 238, 208, 236, 78, 79 }
                        },
                        new
                        {
                            Id = 4,
                            Email = "ivo.ivic@gmail.com",
                            HashedPassword = new byte[] { 114, 249, 61, 150, 143, 251, 4, 235, 230, 145, 235, 120, 212, 214, 210, 3, 250, 71, 220, 219, 83, 14, 223, 70, 37, 120, 0, 22, 224, 94, 199, 185, 210, 195, 179, 224, 38, 139, 87, 100, 211, 46, 13, 43, 34, 78, 142, 227, 215, 254, 15, 233, 8, 143, 114, 187, 91, 154, 221, 93, 243, 255, 226, 193 }
                        },
                        new
                        {
                            Id = 5,
                            Email = "marko.caric@dump.com",
                            HashedPassword = new byte[] { 196, 62, 173, 172, 129, 87, 168, 107, 238, 129, 54, 193, 161, 200, 83, 147, 147, 167, 234, 176, 39, 121, 87, 179, 180, 18, 129, 111, 240, 42, 40, 85, 200, 212, 64, 100, 1, 169, 67, 14, 169, 34, 213, 26, 111, 231, 59, 127, 78, 222, 100, 212, 97, 33, 170, 240, 201, 189, 201, 81, 211, 147, 254, 181 }
                        },
                        new
                        {
                            Id = 6,
                            Email = "kate.bulj@gmail.com",
                            HashedPassword = new byte[] { 18, 112, 192, 234, 241, 86, 84, 138, 22, 49, 152, 254, 22, 157, 31, 233, 36, 250, 90, 169, 13, 68, 111, 128, 93, 29, 206, 181, 32, 128, 190, 55, 173, 11, 106, 171, 21, 130, 12, 66, 134, 7, 52, 224, 101, 18, 44, 179, 47, 35, 108, 17, 74, 136, 11, 182, 218, 93, 197, 6, 109, 183, 83, 219 }
                        });
                });

            modelBuilder.Entity("DmailApp.Data.Entities.Models.Mail", b =>
                {
                    b.HasOne("DmailApp.Data.Entities.Models.User", "Sender")
                        .WithMany("SentMail")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("DmailApp.Data.Entities.Models.ReceiverMail", b =>
                {
                    b.HasOne("DmailApp.Data.Entities.Models.Mail", "Mail")
                        .WithMany("Recipients")
                        .HasForeignKey("MailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DmailApp.Data.Entities.Models.User", "Receiver")
                        .WithMany("ReceivedMail")
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mail");

                    b.Navigation("Receiver");
                });

            modelBuilder.Entity("DmailApp.Data.Entities.Models.Spam", b =>
                {
                    b.HasOne("DmailApp.Data.Entities.Models.User", "SpamUser")
                        .WithMany()
                        .HasForeignKey("SpamUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DmailApp.Data.Entities.Models.User", "User")
                        .WithMany("SpamUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SpamUser");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DmailApp.Data.Entities.Models.Mail", b =>
                {
                    b.Navigation("Recipients");
                });

            modelBuilder.Entity("DmailApp.Data.Entities.Models.User", b =>
                {
                    b.Navigation("ReceivedMail");

                    b.Navigation("SentMail");

                    b.Navigation("SpamUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
