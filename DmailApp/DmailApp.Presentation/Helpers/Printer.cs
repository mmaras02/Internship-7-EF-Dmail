using DmailApp.Data.Entities.Enums;
using DmailApp.Data.Entities.Models;
using DmailApp.Domain.Enums;
using DmailApp.Domain.Factories;
using DmailApp.Domain.Repositories;
using DmailApp.Presentation.Helpers;

namespace DmailApp.Presentation.Helpers;

public static class Printer
{
    public static void ConfirmMessage(string message, ResponseResultType messageType)
    {
        Console.ForegroundColor = ConsoleColor.Black;
        Console.BackgroundColor = ConsoleColor.White;

        if (messageType == ResponseResultType.Error)
            Console.BackgroundColor = ConsoleColor.Red;

        if (messageType == ResponseResultType.Success)
            Console.BackgroundColor = ConsoleColor.Green;

        Console.WriteLine($"{message}, press any key to continue");
        Console.ReadKey();
        Console.ResetColor();
        Console.Clear();
    }
    public static void PrintTitle(string message)
    {
        Console.Clear();
        Console.BackgroundColor = ConsoleColor.Gray;
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
        Console.WriteLine("Actions available\n1.Profile\n2.Primary mail\n3.Sent mail\n4.Spam mail\n5.Send new mail\n6.Send new event\n7.Log out\n'quit'-Exit the app");
    }
    public static ResponseResultType PrintSpamMail(int userId)
    {
        PrintTitle("Spam page");
        var spamRepository = RepositoryFactory.Create<SpamRepository>();
        var userRepository = RepositoryFactory.Create<UserRepository>();

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
}
