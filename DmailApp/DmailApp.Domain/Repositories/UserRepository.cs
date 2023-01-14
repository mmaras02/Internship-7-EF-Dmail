using Microsoft.EntityFrameworkCore;
using DmailApp.Data.Entities;
using DmailApp.Data.Entities.Models;
using DmailApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using DmailApp.Domain.Factories;
using System.Text.RegularExpressions;

namespace DmailApp.Domain.Repositories;

public class UserRepository : BaseRepository
{
    public UserRepository(DmailAppDbContext dbContext) : base(dbContext) { }

    public (int, ResponseResultType) Add(string email, string password)
    {
        var user = new User { Email = email, HashedPassword = PasswordHashed(password) };

        DbContext.Users.Add(user);

        var status = SaveChanges();
        var newId = DbContext.Users.Where(u => u.Email == email).First().Id;

        return (newId, status);
    }
    public ResponseResultType Delete(int id)
    {
        var userToDelete = DbContext.Users.Find(id);
        if (userToDelete is null)
            return ResponseResultType.NotFound;

        DbContext.Users.Remove(userToDelete);

        return SaveChanges();
    }
    public ResponseResultType Edit(User user, int userId)
    {
        var userToEdit = DbContext.Users.Find(userId);
        if (userToEdit is null)
            return ResponseResultType.NotFound;

        userToEdit.Email = user.Email;
        userToEdit.Password = user.Password;

        return SaveChanges();
    }
    public ResponseResultType ValidateEmail(string email)
    {
        if (!Regex.IsMatch(email, "^([a-z A-Z 0-9 .]{1,})+@([a-z A-Z 0-9]{3,})+.+[a-z A-z]{2,}$"))
            return ResponseResultType.ValidationError;

        return ResponseResultType.Success;
    }
    public bool DoesEmailExists(string email) =>
            DbContext.Users.Where(u => u.Email == email).Any();
    public bool CheckPassword(string email, string password) =>
            DbContext.Users
                     .Where(u => u.Email == email && u.HashedPassword == PasswordHashed(password)).Any();

    public User? GetById(int id) => DbContext.Users.FirstOrDefault(u => u.Id == id);
    public User? GetByEmail(string email) => GetAll().FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
    public ICollection<User> GetAll() => DbContext.Users.ToList();
    public int GetIdByEmail(string email) =>
            DbContext.Users.Where(u => u.Email == email).FirstOrDefault().Id;

    public ResponseResultType CheckLogin(string email, string password)
    {
        User? user = DbContext.Users.FirstOrDefault(u => u.Email == email);

        if (user == null)
            return ResponseResultType.NotFound;

        if ((DateTime.UtcNow - user.FailedLogin) < TimeSpan.FromSeconds(30))
            return ResponseResultType.Error;

        if (PasswordHashed(password) != user.HashedPassword)
        {
            user.FailedLogin=DateTime.UtcNow;
            DbContext.SaveChanges();
            return ResponseResultType.ValidationError;
        }
        return ResponseResultType.Success;
    }
    private byte[] PasswordHashed(string password)
    {
        var data = Encoding.UTF8.GetBytes(password);
        var shaM = new SHA512Managed();
        return shaM.ComputeHash(data);
    }
    public List<int> GetSendersByReceiver(int userId)
    {
        var senderIds = DbContext.Recipients
            .Where(rm => rm.ReceiverId == userId)
            .Select(rm => rm.Mail.SenderId)
            .ToList();
    
        return senderIds;
    }
    public List<int> GetRecipientsBySender(int userId)
    {
        var recipientIds = DbContext.Recipients
            .Where(rm => rm.Mail.SenderId == userId)
            .Select(rm => rm.ReceiverId)
            .ToList();
        return recipientIds;
    }
}
