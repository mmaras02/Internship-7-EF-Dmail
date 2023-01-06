using Microsoft.EntityFrameworkCore;
using DmailApp.Data.Entities;
using DmailApp.Data.Entities.Models;
using StackInternship.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

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

    public bool DoesEmailExists(string email) =>
            DbContext.Users.Where(u => u.Email == email).Any();
    public bool CheckUserLogin(string email, string password) =>
            DbContext.Users
                     .Where(u => u.Email == email && u.HashedPassword == PasswordHashed(password)).Any();

    public User? GetById(int id) => DbContext.Users.FirstOrDefault(u => u.Id == id);
    public User? GetByEmail(string email) => GetAll().FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
    public ICollection<User> GetAll() => DbContext.Users.ToList();
    public int GetIdByEmail(string email) =>
            DbContext.Users.Where(u => u.Email == email).FirstOrDefault().Id;


    private byte[] PasswordHashed(string password)
    {
        var data = Encoding.UTF8.GetBytes(password);
        var shaM = new SHA512Managed();
        return shaM.ComputeHash(data);
    }

    //check combination of username/password
}
