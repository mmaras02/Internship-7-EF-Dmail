using DmailApp.Data.Entities;
using DmailApp.Data.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DmailApp.Domain.Enums;
using DmailApp.Data.Entities.Enums;
using System.Text.RegularExpressions;

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
    public ICollection<User> GetRecipients(int mailId) => DbContext.Recipients.Where(r => r.MailId == mailId)
        .Join(DbContext.Users,
        rm => rm.ReceiverId,
        u => u.Id,
        (rm, u) => new { rm, u })
        .Select(a => a.u)
        .ToList();

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
    

    //F-on to get read mail
    //get unread mail
    //get spam mail
    //search mail
    //search spam
    //get sent mail
}
