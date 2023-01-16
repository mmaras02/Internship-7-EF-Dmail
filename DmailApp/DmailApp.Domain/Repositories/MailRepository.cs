using DmailApp.Data.Entities;
using DmailApp.Data.Entities.Models;
using Microsoft.EntityFrameworkCore;
using DmailApp.Domain.Enums;
using DmailApp.Data.Entities.Enums;
using DmailApp.Domain.Factories;

namespace DmailApp.Domain.Repositories;

public class MailRepository : BaseRepository
{
    public MailRepository(DmailAppDbContext dbContext) : base(dbContext) { }
    public static UserRepository? userRepository = RepositoryFactory.Create<UserRepository>();
    public ResponseResultType Add(Mail mail)
    {
        if (mail.Title.Length == 0)
            return ResponseResultType.ValidationError;

        DbContext.Mails.Add(mail);

        return SaveChanges();
    }

    public ResponseResultType Delete(int id)
    {
        var mailToDelete = DbContext.Mails.FirstOrDefault(u => u.MailId == id);

        if (mailToDelete is null)
            return ResponseResultType.NotFound;

        DbContext.Mails.Remove(mailToDelete);

        return SaveChanges();
    }

    public ResponseResultType Update(Mail mail, int id)
    {
        var mailToUpdate = DbContext.Mails.Find(id);
        if (mailToUpdate is null)
            return ResponseResultType.NotFound;

        mailToUpdate.Title = mail.Title;
        mailToUpdate.TimeOfSending = mail.TimeOfSending;
        mailToUpdate.SenderId = mail.SenderId;

        return SaveChanges();
    }
    public ICollection<Mail> GetAll() => DbContext.Mails
            .Include(m => m.Sender)
            .Include(m => m.Recipients)
            .ToList();
    public ICollection<Mail> GetEmails() => GetAll().Where(m=>m.MailType==MailType.MessageMail).ToList();
    public ICollection<Mail> GetEvents() => GetAll().Where(m=>m.MailType==MailType.EventMail).ToList();
    public ICollection<Mail> GetSender(int id) => GetAll().Where(m => m.SenderId == id).ToList();
    public Mail? GetById(int id) => DbContext.Mails.FirstOrDefault(m => m.MailId == id);
   
    public ICollection<User> GetRecipients(int mailId)
    { 
        var recipient = DbContext.Recipients
            .Where(rm => rm.MailId == mailId)
            .ToList();

        List<User> receivers = new List<User>();
        foreach (var user in recipient)
        {
            receivers.Add(userRepository.GetById(user.ReceiverId));
        }
       
        return receivers;
    }
    public List<Mail>GetReadMail(int userId)
    {
        var readMail = GetAll()
            .Join(DbContext.Recipients,
            m => m.MailId,
            rm => rm.MailId, (m, rm) => new { m, rm })
            .Where(a => a.rm.MailStatus == MailStatus.Read)
            .Where(a => a.rm.ReceiverId == userId)
            .Select(a => a.m).ToList();

        List<Mail> readList = new List<Mail>();
        foreach (var item in readMail)
        {
            if (DbContext.SpamFlag.Find(userRepository.GetById(userId).Id,item.SenderId) is null)
                readList.Add(item);
        }
        return readList;
    }
    public List<Mail> GetUnreadMail(int userId)
    {
        var unreadMail = GetAll()
            .Join(DbContext.Recipients,
            m => m.MailId,
            rm => rm.MailId, (m, rm) => new { m, rm })
            .Where(a => a.rm.MailStatus == MailStatus.Unread)
            .Where(a => a.rm.ReceiverId == userId)
            .Select(a => a.m).ToList();

        List<Mail> unreadList = new List<Mail>();
        foreach (var item in unreadMail)
        {
            if (DbContext.SpamFlag.Find(userRepository.GetById(userId).Id, item.SenderId)is null)
                unreadList.Add(item);
        }
        return unreadList;
    }
    public ResponseResultType ChangeToRead(int mailId)
    {
        var mailToUpdate = DbContext.Recipients.FirstOrDefault(rm => rm.MailId == mailId);

        mailToUpdate.MailStatus = MailStatus.Read;

        return SaveChanges();
    }
    public ResponseResultType ChangeToUnread(Mail mail)
    {
        var mailToUpdate = DbContext.Recipients.FirstOrDefault(rm => rm.MailId == mail.MailId);
        if (mailToUpdate is null)
            return ResponseResultType.NoChanges;
    
        mailToUpdate.MailStatus = MailStatus.Unread;
        return SaveChanges();
    }
    public EventStatus ChangeEventStatus(Mail mail,EventStatus status)
    {
        var eventToUpdate = DbContext.Recipients.FirstOrDefault(rm => rm.MailId == mail.MailId);
        if (eventToUpdate is null)
            return EventStatus.NoResponse;

        eventToUpdate.EventStatus = status;
        return (EventStatus)SaveChanges();
    }
    public EventStatus GetEventStatus(int mailId)
    {
        var status = DbContext.Recipients.FirstOrDefault(rm => rm.MailId == mailId);
        return (EventStatus)status.EventStatus;
    }
    public ICollection<Mail>GetSentMail(int userId)
    {
        var sent=DbContext.Mails
            .Where(m=>m.SenderId==userId).ToList();

        return sent;
    }
    public int GetFreeId()
    {
        var userIds = DbContext.Mails
            .Select(m => m.MailId)
            .ToList();
        return userIds.Last() + 1;
    }
    public List<Mail>SearchByString(int userId,string query)
    {
        var result=DbContext.Recipients
            .Where(rm=>rm.ReceiverId==userId)
            .Join(DbContext.Mails,rm=>rm.MailId,m=>m.MailId,(rm,m)=>new {rm,m})
            .Where(a=>a.m.Sender.Email.Contains(query))
            .Select(a=>a.m)
            .OrderByDescending(m=>m.TimeOfSending)
            .ToList();

        return result;
    }
    public ResponseResultType NewMail(int userId,int receiverId)
    {
        Console.WriteLine("Enter title: ");
        var title = Console.ReadLine();

        Console.WriteLine("Enter mail content: ");
        var content = Console.ReadLine();

        Mail newMail = new(title)
        {
            MailId = GetFreeId(),
            SenderId = userId,
            MailType = MailType.MessageMail,
            TimeOfSending = DateTime.UtcNow.Date,
            Content = content
        };

        Add(newMail);

        Console.WriteLine("Are you sure you want to send this mail? (y/n)");
        if (Console.ReadLine() != "y")
            return ResponseResultType.Error;

        ReceiverMail newReceiverMail = new()
        {
            MailId = newMail.MailId,
            ReceiverId = receiverId,
            MailStatus = MailStatus.Unread
        };
        var receiverMailRepository = RepositoryFactory.Create<ReceiverMailRepository>();
        receiverMailRepository.Add(newReceiverMail);

        return ResponseResultType.Success;
    }
}
