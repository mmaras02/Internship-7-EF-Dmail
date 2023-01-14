using DmailApp.Data.Entities.Enums;
using DmailApp.Data.Entities.Models;
using DmailApp.Domain.Enums;
using DmailApp.Domain.Factories;
using DmailApp.Domain.Repositories;
using DmailApp.Presentation.Entities.Actions;
using DmailApp.Presentation.Helpers;

namespace DmailApp.Presentation.Helpers;

public static class Printer
{
    public static UserRepository? userRepository = RepositoryFactory.Create<UserRepository>();
    public static MailRepository? mailRepository = RepositoryFactory.Create<MailRepository>();
    public static SpamRepository? spamRepository = RepositoryFactory.Create<SpamRepository>();
    public static void ConfirmMessage(string message, ResponseResultType messageType)
    {
        Console.ForegroundColor = ConsoleColor.Black;
        Console.BackgroundColor = ConsoleColor.White;

        if (messageType == ResponseResultType.Error)
            Console.BackgroundColor = ConsoleColor.Red;

        if (messageType == ResponseResultType.Success)
            Console.BackgroundColor = ConsoleColor.Green;

        if(messageType==ResponseResultType.NoChanges)
            Console.BackgroundColor = ConsoleColor.Yellow;

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
        PrintTitle("Inbox Mail");
        Console.WriteLine("\n1.Read mail\n2.Unread mail\n3.Search mail from user\n0.Back to main menu\n");
    }
    public static ResponseResultType PrintSpamMail(int userId)
    {
        PrintTitle("Spam page");

        var spam = spamRepository.GetAllSpam(userId).ToList();
        List<int> spamIds = new List<int>();

        foreach (var item in spam)
            spamIds.Add(item.SpamUserId);

        if(spamIds.Count is 0 )
        {
            ConfirmMessage("you don't have any users marked as spam", ResponseResultType.Error);
            return ResponseResultType.Error;
        }
        Console.WriteLine("List of users you marked as spam:");
        foreach (var item in spamIds)
            Console.WriteLine(userRepository.GetById(item).Email);

        return ResponseResultType.Success;
    }
    public static void ReadMail(int userId, List<Mail> mail)
    {
        Console.Clear();
        var index = 0;

        foreach (var item in mail)
        {
            Console.WriteLine($"{++index}. {item.Title} - {userRepository.GetById(item.SenderId).Email}");
        }
        if (index is 0)
        {
            ConfirmMessage("You don't have any mails in this container!", ResponseResultType.NoChanges);
            return;
        }
        ChosenEmail(userId, index, mail);
    }
    public static void ChosenEmail(int userId,int index,List<Mail> mail)
    {
        Console.WriteLine("\nEnter the number of the message you want to enter or enter 0 for returning to main menu");
        Checker.CheckNumber(Console.ReadLine(), out var input);
        if (input is 0)
            return;

        if (input > 0 && input <= index)
            PrintMail(mail[input - 1].MailId, userId);

        else
            ConfirmMessage("Incorrect input! ", ResponseResultType.Error);
    }
    public static void PrintMail(int mailId, int userId)
    {
        Mail mail = mailRepository.GetById(mailId);

        mailRepository.ChangeToRead(mail);
        var sender = userRepository.GetById(mail.SenderId);

        if(mail.MailType is MailType.MessageMail)
        {
            Console.WriteLine($"Title: {mail.Title}\nTime of sending: {mail.TimeOfSending}\nSender: {sender.Email}\n");
            Console.WriteLine($"{mail.Content}\n");
        }
        else if (mail.MailType is MailType.EventMail)
        {
            Console.WriteLine($"Title: {mail.Title}\nTime of sending: {mail.TimeOfSending}\nSender: {sender.Email}\n");
            Console.WriteLine($"Event start: {mail.EventTime}\nEvent duration: {mail.EventDuration}");

            Console.WriteLine("\nList of  other invites");
            var recipients=mailRepository.GetRecipients(mail.MailId);

            foreach ( var recipient in recipients)
            {
                Console.WriteLine($"{recipient.Email}");
            }
        }

    }
}
