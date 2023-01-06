using System;
using DmailApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DmailApp.Data.Migrations
{
    [DbContext(typeof(DmailAppDbContext))]
    partial class DmailAppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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
                });

            modelBuilder.Entity("DmailApp.Data.Entities.Models.SpamFlag", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("SpamUserId")
                        .HasColumnType("integer");

                    b.HasKey("UserId", "SpamUserId");

                    b.HasIndex("SpamUserId");

                    b.ToTable("SpamFlag");
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

            modelBuilder.Entity("DmailApp.Data.Entities.Models.SpamFlag", b =>
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
