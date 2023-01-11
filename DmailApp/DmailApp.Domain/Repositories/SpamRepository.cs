using DmailApp.Domain.Enums;
using DmailApp.Data.Entities;
using DmailApp.Data.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmailApp.Domain.Repositories;

public class SpamRepository : BaseRepository
{
    public SpamRepository(DmailAppDbContext dbContext) : base(dbContext) { }

    public ResponseResultType Add(SpamFlag spamUser)
    {
        DbContext.SpamFlag.Add(spamUser);

        return SaveChanges();
    }

    public ResponseResultType Delete(int userId, int spamId)
    {
        var spamUserToDelete = DbContext.SpamFlag.FirstOrDefault(u => u.UserId == userId && u.SpamUserId == spamId);

        if (spamUserToDelete is null)
            return ResponseResultType.NotFound;

        DbContext.SpamFlag.Remove(spamUserToDelete);

        return SaveChanges();
    }
    //get list of spam users
}
