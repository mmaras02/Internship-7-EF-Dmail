using DmailApp.Data.Entities;
using DmailApp.Data.Entities.Enums;
using DmailApp.Data.Entities.Models;
using DmailApp.Domain.Enums;

namespace DmailApp.Domain.Repositories;

public class SpamRepository : BaseRepository
{
    public SpamRepository(DmailAppDbContext dbContext) : base(dbContext) { }

    public ICollection<SpamFlag> GetAll() => DbContext.SpamFlag.ToList();
    public bool DoesSpamPairExist(int userId, int spamId) => GetAll().Any(sf => sf.UserId == userId && sf.SpamUserId == spamId);

    public ResponseResultType MarkSpam(int userId, int spamUserId)
    {

        if (DoesSpamPairExist(userId, spamUserId))
            return ResponseResultType.NoChanges;

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
    public List<Mail> GetUnreadSpamMail(int userId)
    {
        var unreadSpam = DbContext.Recipients
            .Where(rm => rm.ReceiverId == userId)
            .Where(rm => rm.MailStatus == MailStatus.Unread)
            .Join(DbContext.Mails, rm => rm.MailId, m => m.MailId, (rm, m) => new { rm, m })
            .Select(a => a.m)
            .OrderByDescending(m => m.TimeOfSending)
            .ToList();

        List<Mail> unreadList = new List<Mail>();
        foreach (var item in unreadSpam)
        {
            if (DoesSpamPairExist(userId, item.SenderId))
                unreadList.Add(item);
        }
        return unreadList;
    }
    public List<Mail> GetReadSpamMail(int userId)
    {
        var readSpam = DbContext.Recipients
            .Where(rm => rm.ReceiverId == userId)
            .Where(rm => rm.MailStatus == MailStatus.Read)
            .Join(DbContext.Mails, rm => rm.MailId, m => m.MailId, (rm, m) => new { rm, m })
            .Select(a => a.m)
            .OrderByDescending(m => m.TimeOfSending)
            .ToList();

        List<Mail> readList = new List<Mail>();
        foreach (var item in readSpam)
        {
            if (DoesSpamPairExist(userId, item.SenderId))
                readList.Add(item);
        }
        return readList;
    }
}
