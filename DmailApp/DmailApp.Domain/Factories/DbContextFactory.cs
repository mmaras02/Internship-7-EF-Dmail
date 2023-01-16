using DmailApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace DmailApp.Domain.Factories;

public static class DbContextFactory
{
    public static DmailAppDbContext GetDmailAppDbContext()
    {
        var options = new DbContextOptionsBuilder()
            .UseNpgsql(ConfigurationManager.ConnectionStrings["DmailApp"].ConnectionString)
            .Options;

        return new DmailAppDbContext(options);
    }
}
