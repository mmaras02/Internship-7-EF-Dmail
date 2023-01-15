using DmailApp.Data.Entities;
using DmailApp.Data.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using DmailApp.Domain.Enums;
using DmailApp.Data.Entities.Enums;
using System.Text.RegularExpressions;
using DmailApp.Domain.Factories;
using System.Drawing.Printing;
using System.Collections;

namespace DmailApp.Domain.Repositories;

public class MailRepository : BaseRepository
{
    public MailRepository(DmailAppDbContext dbContext) : base(dbContext) { }

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
        var userRepository = RepositoryFactory.Create<UserRepository>();

        foreach (var user in recipient)
        {
            receivers.Add(userRepository.GetById(user.ReceiverId));
        }
       
        return receivers;
    }
    public ICollection<Mail> GetSpamMail(int userId)
    {
        var spamMail = GetAll()
            .Join(DbContext.Recipients,
                m => m.MailId,
                rm => rm.MailId,
                (m, rm) => new { m, rm })
            .Where(a => a.rm.ReceiverId == userId)
            .Select(a => a.m).ToList()
            .Join(DbContext.SpamFlag,
                m => m.SenderId,
                sf => sf.UserId,
                (m, sf) => new { m, sf })
            .Select(a => a.m).ToList();

        return spamMail;
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
        return readMail;
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
        return unreadMail;
    }
    public ResponseResultType ChangeToRead(Mail mail)
    {
        var mailToUpdate = DbContext.Recipients.FirstOrDefault(rm => rm.MailId == mail.MailId);
        if (mailToUpdate is null)
            return ResponseResultType.NoChanges;

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

    //search spam
    //get sent mail
}
