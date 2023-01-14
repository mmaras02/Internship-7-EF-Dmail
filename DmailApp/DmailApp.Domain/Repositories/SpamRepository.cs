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
        if (DbContext.Users.Find(spamUser.UserId) is null)
            return ResponseResultType.NotFound;

        if (DbContext.Users.Find(spamUser.SpamUserId) is null)
            return ResponseResultType.NotFound;

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

    public ResponseResultType MarkSpam(int userId,int spamUserId)
    {
        DbContext.SpamFlag.Add(new SpamFlag()
        {
            UserId = userId,
            SpamUserId = spamUserId,
        
        });
        return SaveChanges();
    }
    public ResponseResultType RemoveSpam(int userId, int spamUserId)
    {
        var adressToRemove = DbContext.SpamFlag
                .Where(sf => sf.UserId == userId)
                .Where(sf => sf.SpamUserId == spamUserId)
                .ToList();

        DbContext.SpamFlag.Remove(adressToRemove[0]);
        return SaveChanges();
    }
    public List<SpamFlag> GetAllSpam(int userId)
    {
        var allSpam = DbContext.SpamFlag
            .Where(sf => sf.UserId == userId)
            .ToList();

        return allSpam;
    }
    public List<int> GetSpamIdsList(int userId)
    {
        List<int> spamList = new List<int>();
        var spam = GetAllSpam(userId);
        foreach (var item in spam)
            spamList.Add(item.SpamUserId);
        return spamList;
    }
}
