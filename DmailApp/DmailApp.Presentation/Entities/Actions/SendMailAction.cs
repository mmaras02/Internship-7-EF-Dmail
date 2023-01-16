using DmailApp.Data.Entities.Enums;
using DmailApp.Data.Entities.Models;
using DmailApp.Domain.Enums;
using DmailApp.Domain.Factories;
using DmailApp.Domain.Repositories;
using DmailApp.Presentation.Entities.Interfaces;

namespace DmailApp.Presentation.Entities.Actions;

public class SendMailAction : IAction
{
    public int UserId;
    public IAction Action()
    {
        PrintTitle("New mail");
        var userRepository = RepositoryFactory.Create<UserRepository>();
        var mailRepository = RepositoryFactory.Create<MailRepository>();
        var receiverMailRepository = RepositoryFactory.Create<ReceiverMailRepository>();

        Console.WriteLine("Enter title: ");
        var validTitle = CheckString(Console.ReadLine(), out string title);
        
        Console.WriteLine("Enter mail content: ");
        var validContent=CheckString(Console.ReadLine(), out string content);

        if (!validContent || !validContent)
            PrintMessage("Wrong input! Can't be empty field", ResponseResultType.Error);

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
        string[] emails = Console.ReadLine().Split(", ");

        if (!GetConfirmation("send this mail? "))
        {
            PrintMessage("Sending stopped", ResponseResultType.NoChanges);
            return new HomePageAction { UserId= UserId };
        }

        foreach(var item in emails)
        {
            if (!userRepository.DoesEmailExists(item))
                PrintMessage($"{item} doesn't exist! ", ResponseResultType.Warning);
            
            ReceiverMail newReceiverMail = new()
            {
                MailId = newMail.MailId,
                ReceiverId = userRepository.GetIdByEmail(item),
                MailStatus = MailStatus.Unread
            };
            receiverMailRepository.Add(newReceiverMail);
        }
        PrintMessage("Message sent successfuly!",ResponseResultType.Success);
        return new HomePageAction { UserId= UserId };
    }
}
