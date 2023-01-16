using DmailApp.Domain.Enums;
using DmailApp.Domain.Factories;
using DmailApp.Domain.Repositories;
using DmailApp.Presentation.Entities.Interfaces;

namespace DmailApp.Presentation.Entities.Actions;

public class ProfileSettingsAction : IAction
{
    public int UserId;
    public IAction Action()
    {
        var userRepository = RepositoryFactory.Create<UserRepository>();
        var spamRepository = RepositoryFactory.Create<SpamRepository>();

        PrintTitle("Profile settings");
        var markedSpam = spamRepository.GetSpamIdsList(UserId);

        Console.WriteLine("\nList of users that sent you mails: ");
        var senderIds = userRepository.GetSendersByReceiver(UserId).Distinct().ToList();
        PrintUsers(senderIds, markedSpam);

        Console.WriteLine("\nList of users that you sent mails to: ");//boja?
        var receiversIds = userRepository.GetRecipientsBySender(UserId).Distinct().ToList();
        PrintUsers(receiversIds, markedSpam);

        List<int> everyId = receiversIds.Concat(senderIds).Distinct().ToList();

        if (everyId.Contains(UserId))
            everyId.Remove(UserId);

        if (!GetConfirmation("acces user? "))
        {
            PrintMessage("Going back to home page...", ResponseResultType.Success);
            return new HomePageAction { UserId = UserId };
        }
        Console.Clear();
        var index = 0;

        Console.WriteLine("\nList of all users you have:");
        foreach (var item in everyId)
        {
            Console.WriteLine($"{++index}.{userRepository.GetById(item).Email}");
        }

        Console.WriteLine("\nEnter number you want to change ");
        var input = NumberInput(maxNumber: index);

        if (!markedSpam.Contains(everyId[input - 1]))
        {
            spamRepository.MarkSpam(UserId, userRepository.GetById(everyId[input - 1]).Id);
            PrintMessage("Successfully marked as spam!\nGoing back to main menu ", ResponseResultType.Success);
            return new HomePageAction { UserId = UserId };
        }
        else if (markedSpam.Contains(everyId[input - 1]))
        {
            spamRepository.RemoveSpam(UserId, userRepository.GetById(everyId[input - 1]).Id);
            PrintMessage("Successfully  removed spam!\nGoing back to main menu", ResponseResultType.Success);
        }

        return new HomePageAction { UserId = UserId };
    }
}
