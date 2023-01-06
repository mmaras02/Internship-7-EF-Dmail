using Microsoft.EntityFrameworkCore;
using System.Configuration;
using DmailApp.Data.Entities;

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
