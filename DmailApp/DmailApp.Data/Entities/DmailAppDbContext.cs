using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using DmailApp.Data.Entities.Models;
using DmailApp.Data.Seeds;
using System.Configuration;
using System;

namespace DmailApp.Data.Entities;
public class DmailAppDbContext : DbContext
{
    public DmailAppDbContext(DbContextOptions options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Mail> Mails => Set<Mail>();
    public DbSet<SpamFlag> SpamFlag => Set<SpamFlag>();
    public DbSet<ReceiverMail> Recipients => Set<ReceiverMail>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Mail>()
            .HasDiscriminator<string>("mail_type")
            .HasValue<Mail>("mail");

        modelBuilder.Entity<Mail>()
           .HasOne(m => m.Sender)
           .WithMany(u => u.SentMail)
           .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ReceiverMail>()
            .HasKey(r => new { r.ReceiverId, r.MailId });

        modelBuilder.Entity<ReceiverMail>()
            .HasOne(rm => rm.Mail)
            .WithMany(m => m.Recipients)
            .HasForeignKey(rm => rm.MailId);

        modelBuilder.Entity<ReceiverMail>()
            .HasOne(rm => rm.Receiver)
            .WithMany(u => u.ReceivedMail)
            .HasForeignKey(rm => rm.ReceiverId);

        modelBuilder.Entity<User>()
            .HasDiscriminator<string>("user_type")
            .HasValue<User>("user");

        modelBuilder.Entity<SpamFlag>()
            .HasKey(s => new { s.UserId, s.SpamUserId });

        modelBuilder.Entity<SpamFlag>()
            .HasOne(s => s.User)
            .WithMany(u => u.SpamUsers)
            .HasForeignKey(s => s.UserId);

        DatabaseSeeder.Seed(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }
}
public class DmailAppDbContextFactory : IDesignTimeDbContextFactory<DmailAppDbContext>
{
    public DmailAppDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddXmlFile("App.config")
            .Build();

        config.Providers
            .First()
            .TryGet("connectionStrings:add:DmailApp:connectionString", out var connectionString);

        var options = new DbContextOptionsBuilder<DmailAppDbContext>()
            .UseNpgsql(connectionString)
            .Options;

        return new DmailAppDbContext(options);
    }
}
