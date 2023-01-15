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

public class SendEventAction : IAction
{
    public int UserId;

    public IAction Action()
    {
        Printer.PrintTitle("New event");
        var userRepository = RepositoryFactory.Create<UserRepository>();
        var mailRepository = RepositoryFactory.Create<MailRepository>();
        var receiverMailRepository = RepositoryFactory.Create<ReceiverMailRepository>();

        Console.WriteLine("Enter title: ");
        var validTitle = Checker.CheckString(Console.ReadLine(), out string title);
        
        Console.WriteLine("Enter event date start (yyyy-MM-dd HH:mm:ss): ");
        var validDate = DateTime.TryParse(Console.ReadLine(), out DateTime dateInput);

        Console.WriteLine("Enter event duration (HH:mm:ss): ");
        var validDuration = TimeSpan.TryParse(Console.ReadLine(), out TimeSpan durationInput);

        if (!validDate || dateInput < DateTime.Now)
        {
            PrintMessage("Incorrect date input! Try again", ResponseResultType.Error);
            return null;
        }
        Mail newMail = new(title)
        {
            MailId = mailRepository.GetFreeId(),
            SenderId = UserId,
            MailType = MailType.MessageMail,
            TimeOfSending = DateTime.UtcNow.Date,
            EventTime=dateInput,
            EventDuration=durationInput,
        };

        mailRepository.Add(newMail);

        Console.WriteLine("Enter receivers emails (separate with <, >):");
        string[] emails = Console.ReadLine().Split(", ");

        if (!GetConfirmation("send this event? " ))
        {
            PrintMessage("Sending stopped", ResponseResultType.Warning);
            return new HomePageAction { UserId = UserId };
        }
        foreach (var item in emails)
        {
            if (!userRepository.DoesEmailExists(item))
                PrintMessage($"{item} doesn't exist! ", ResponseResultType.Warning);

            ReceiverMail newReceiverMail = new()
            {
                MailId = newMail.MailId,
                ReceiverId = userRepository.GetIdByEmail(item),
                MailStatus = MailStatus.Unread,
                EventStatus=EventStatus.NoResponse
            };
            receiverMailRepository.Add(newReceiverMail);
        }
        PrintMessage("Event invitation sent successfuly!", ResponseResultType.Success);
        return new HomePageAction { UserId = UserId };
    }
}
