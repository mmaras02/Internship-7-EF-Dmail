﻿using DmailApp.Data.Entities.Models;
using DmailApp.Domain.Enums;
using DmailApp.Domain.Factories;
using DmailApp.Domain.Repositories;
using DmailApp.Presentation.Entities.Interfaces;
using DmailApp.Presentation.Helpers;
using System.Text;

namespace DmailApp.Presentation.Entities.Actions;

public class OutgoingMailAction : IAction
{
    public int UserId;
    public IAction Action()
    {
        Printer.PrintTitle("Outgoing mail");
        var userRepository = RepositoryFactory.Create<UserRepository>();
        var mailRepository = RepositoryFactory.Create<MailRepository>();
        var index = 0;

        List<Mail> outgoing = mailRepository.GetSentMail(UserId)
            .OrderByDescending(m=>m.TimeOfSending)
            .ToList();

        foreach (var item in outgoing)
        {
            var recipients = mailRepository.GetRecipients(item.MailId);
            Console.WriteLine($"{++index}. Title: {item.Title}\nSent to: {ReadEmail(recipients)}\n");
        }
        if (index == 0)
            Printer.ConfirmMessage("You didn't send any messages ",ResponseResultType.NoChanges);

        Printer.ChosenEmail(UserId, index, outgoing);

        Checker.UserInput("go back to menu");
        return  new HomePageAction { UserId= UserId };  
    }
    public string ReadEmail(ICollection<User>recipients)
    {
        return string.Join(",", recipients.Select(x => x.Email));
    }
}