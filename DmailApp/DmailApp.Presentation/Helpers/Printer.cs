using DmailApp.Data.Entities.Enums;
using DmailApp.Data.Entities.Models;
using DmailApp.Domain.Enums;
using DmailApp.Domain.Factories;
using DmailApp.Domain.Repositories;
using DmailApp.Presentation.Entities.Actions;
using DmailApp.Presentation.Helpers;
using System.Security.Cryptography.X509Certificates;

namespace DmailApp.Presentation.Helpers;

public static class Printer
{
    public static UserRepository? userRepository = RepositoryFactory.Create<UserRepository>();
    public static MailRepository? mailRepository = RepositoryFactory.Create<MailRepository>();
    public static SpamRepository? spamRepository = RepositoryFactory.Create<SpamRepository>();
    public static void PrintMessage(string message, ResponseResultType messageType)
    {
        Console.ForegroundColor = ConsoleColor.Black;
        Console.BackgroundColor = ConsoleColor.White;

        if (messageType == ResponseResultType.Error)
            Console.BackgroundColor = ConsoleColor.Red;

        if (messageType == ResponseResultType.Success)
            Console.BackgroundColor = ConsoleColor.Green;

        if(messageType==ResponseResultType.NoChanges)
            Console.BackgroundColor = ConsoleColor.Yellow;

        if (messageType == ResponseResultType.Warning)
            Console.BackgroundColor = ConsoleColor.DarkRed;

        Console.WriteLine($"{message}, press any key to continue");
        Console.ReadKey();
        Console.ResetColor();
        Console.Clear();
    }
    public static void PrintTitle(string message)
    {
        Console.Clear();
        Console.BackgroundColor = ConsoleColor.Blue;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine(new String('-', 30));
        Console.WriteLine($"    {message}       ");
        Console.WriteLine(new String('-', 30));
        Console.ResetColor();
    }
    public static void PrintMainMenu()
    {
        Console.WriteLine("Actions available:\n1 - Login\n2 - Registration\n'exit' - Exit the app\n");
    }
    public static void PrintHomePageMenu()
    {
        PrintTitle("Home page");
        Console.WriteLine("Actions available\n1.Primary mail\n2.Outgoing mail\n3.Spam mail\n4.Send new mail\n5.Send new event\n6.Profile settings\n7.Log out\n0.Exit the app");
    }
    public static void PrintInbox()
    {
        Console.WriteLine("\n1.Read mail\n2.Unread mail\n3.Search mail from user\n0.Back to main menu\n");
    }
    public static void PrintOptions()
    {
        Console.WriteLine("\nYour options for choosen mail:");
        Console.WriteLine("1.Mark as unread\n2.Mark as spam\n3.Delete mail\n4.Replay to mail\n0.Go back to primary mail");
    }
    public static void ReadMail(int userId, List<Mail> mail,bool inbox)
    {
        //Console.Clear();
        var index = 0;

        Console.WriteLine("\tTitle\t\tSender");
        foreach (var item in mail)
        {
            Console.WriteLine($"{++index}. {item.Title} - {userRepository.GetById(item.SenderId).Email}");
        }
        if (index is 0)
        {
            PrintMessage("You don't have any mails in this container!", ResponseResultType.NoChanges);
            return;
        }
        FilterMail(userId, index, mail,inbox);
    }
    public static void FilterMail(int userId,int index,List<Mail> mail,bool inbox)
    {
        Console.WriteLine("\nNumber of the mail you want to filter\n0.Go back to main");
        int.TryParse(Console.ReadLine(), out int input);

        if (input is 0)
            return;

        if (input < 0 && input >= index)
            PrintMessage("Incorrect input! ", ResponseResultType.Error);

        PrintDetailedMail(mail[input - 1].MailId, userId);
        var message = mailRepository.GetById(mail[input - 1].MailId);

        if (!inbox)
        {
            GetOutboxActions(message.MailId);
        }
        else
            GetInboxActions(message,userId);
    }
    public static void GetOutboxActions(int mailId)
    {
        if (!GetConfirmation("delete this mail? "))
            return;

        mailRepository.Delete(mailId);
    }
    public static void GetInboxActions(Mail mail,int userId)
    {
        PrintOptions();
        switch(NumberInput(maxNumber:4))
        {
            case 1:
                mailRepository.ChangeToUnread(mail);
                PrintMessage("You changed mail to unread! ",ResponseResultType.Success);
                return;
            case 2:
                if (spamRepository.MarkSpam(userId, mail.SenderId) == ResponseResultType.NoChanges)
                    PrintMessage("User already marked as spam!", ResponseResultType.NoChanges);
                else
                    PrintMessage("Sucessfully marked spam! ", ResponseResultType.Success);
                return;
            case 3:
                mailRepository.Delete(mail.MailId);
                PrintMessage("You deleted choosen mail! ", ResponseResultType.Success);
                return;
            case 4:
                return;
            default:
                return;
        }
    }
    public static void PrintDetailedMail(int mailId, int userId)
    {
        Mail mail = mailRepository.GetById(mailId);

        mailRepository.ChangeToRead(mail);
        var sender = userRepository.GetById(mail.SenderId);

        if (mail.MailType is MailType.MessageMail)
        {
            Console.WriteLine($"Title: {mail.Title}\nTime of sending: {mail.TimeOfSending}\nSender: {sender.Email}\n");
            Console.WriteLine($"{mail.Content}\n");
        }
        else if (mail.MailType is MailType.EventMail)
        {
            Console.WriteLine($"Title: {mail.Title}\nTime of sending: {mail.TimeOfSending}\nSender: {sender.Email}\n");
            Console.WriteLine($"Event start: {mail.EventTime}\nEvent duration: {mail.EventDuration}");

            Console.WriteLine("\nList of  other invites");
            var recipients = mailRepository.GetRecipients(mail.MailId);

            foreach (var recipient in recipients)
            {
                Console.WriteLine($"{recipient.Email}");
            }
        }
    }
    public static void PrintUsers(List<int>userIds,List<int>markedSpam) 
    {
        var index = 0;
        foreach (var item in userIds)
        {
            if (markedSpam.Contains(item))
                Console.WriteLine($"{++index}.{userRepository.GetById(item).Email} (marked as spam)");
            else
                Console.WriteLine($"{++index}.{userRepository.GetById(item).Email}");
            }
        }
}
