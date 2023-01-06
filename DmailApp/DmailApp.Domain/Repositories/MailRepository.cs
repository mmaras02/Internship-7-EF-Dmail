using DmailApp.Data.Entities;
using DmailApp.Data.Entities.Models;
using Microsoft.EntityFrameworkCore;
using StackInternship.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmailApp.Domain.Repositories;

public class MailRepository : BaseRepository
{
    public MailRepository(DmailAppDbContext dbContext) : base(dbContext) { }

    public ResponseResultType Add(Mail mail)
    {
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
            .Include(m => m.Title)
            .Include(m => m.Sender)
            .Include(m => m.Recipients)
            .ToList();
    public ICollection<Mail> GetEmails() => GetAll().ToList();
    //public ICollection<EventMail> GetEventEmails() => (ICollection<EventMail>)GetAll().ToList();
    public ICollection<Mail> GetSender(int id) => GetAll().Where(m => m.SenderId == id).ToList();


    //F-on to get read mail
    //get unread mail
    //get spam mail
    //search mail
    //search spam
    //get sent mail
}
