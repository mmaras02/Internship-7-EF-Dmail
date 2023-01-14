using DmailApp.Data.Entities.Enums;
using DmailApp.Data.Entities.Models;
using DmailApp.Domain.Enums;
using DmailApp.Domain.Factories;
using DmailApp.Domain.Repositories;
using DmailApp.Presentation.Entities.Interfaces;
using DmailApp.Presentation.Helpers;
using Microsoft.EntityFrameworkCore;
using System;


namespace DmailApp.Presentation.Entities.Actions;

public class SendMailAction : IAction
{
    public int UserId;
    public IAction Action()
    {
        Printer.PrintTitle("New mail");
        var userRepository = RepositoryFactory.Create<UserRepository>();
        var mailRepository = RepositoryFactory.Create<MailRepository>();
        var receiverMailRepository = RepositoryFactory.Create<ReceiverMailRepository>();
        var input="";

        Console.WriteLine("Enter title: ");
        var validTitle = Checker.CheckString(Console.ReadLine(), out string title);
        
        Console.WriteLine("Enter mail content: ");
        var validContent=Checker.CheckString(Console.ReadLine(), out string content);

        if (!validContent || !validContent)
            Printer.ConfirmMessage("Wrong input! Can't be empty field", ResponseResultType.Error);

        Mail newMail = new(title)
        {
            MailId= mailRepository.GetFreeId(),
            SenderId = UserId,
            MailType = MailType.MessageMail,
            TimeOfSending = DateTime.UtcNow.Date,
            Content=content
        };

        mailRepository.Add(newMail);

        Console.WriteLine("Enter receivers emails (separate with <, >):");
        var receivers = Console.ReadLine();

        string[] emails = receivers.Split(", ");

        Console.WriteLine("Enter <y> to confirm sending this mail");
        input=Console.ReadLine();

        if (input != "y")
        {
            Printer.ConfirmMessage("Sending stopped", ResponseResultType.NoChanges);
            return new HomePageAction { UserId= UserId };
        }

        foreach(var item in emails)
        {
            if (!userRepository.DoesEmailExists(item))
                Printer.ConfirmMessage($"{item} doesn't exist! ", ResponseResultType.Error);
            
            ReceiverMail newReceiverMail = new()
            {
                MailId = newMail.MailId,
                ReceiverId = userRepository.GetIdByEmail(item),
                MailStatus = MailStatus.Unread
            };
            receiverMailRepository.Add(newReceiverMail);
        }
        Printer.ConfirmMessage("Message sent successfuly!",ResponseResultType.Success);
        return new HomePageAction { UserId= UserId };
    }
}
