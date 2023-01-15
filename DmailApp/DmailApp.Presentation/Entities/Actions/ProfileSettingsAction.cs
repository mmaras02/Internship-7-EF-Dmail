﻿using DmailApp.Domain.Enums;
using DmailApp.Domain.Factories;
using DmailApp.Domain.Repositories;
using DmailApp.Presentation.Entities.Interfaces;
using DmailApp.Presentation.Helpers;
using System;
using System.Linq;

namespace DmailApp.Presentation.Entities.Actions;

public class ProfileSettingsAction : IAction
{
    public int UserId;
    public IAction Action()
    {
        var userRepository = RepositoryFactory.Create<UserRepository>();
        var mailRepository = RepositoryFactory.Create<MailRepository>();
        var spamRepository = RepositoryFactory.Create<SpamRepository>();

        PrintTitle("Profile settings");
        var markedSpam = spamRepository.GetSpamIdsList(UserId);

        Console.WriteLine("\nList of users that sent you mails: ");
        var senderIds = userRepository.GetSendersByReceiver(UserId).Distinct().ToList();
        PrintUsers(senderIds,markedSpam);

        Console.WriteLine("\nList of users that you sent mails to: ");//boja?
        var receiversIds = userRepository.GetRecipientsBySender(UserId).Distinct().ToList();
        PrintUsers(receiversIds, markedSpam);

        List<int> everyId = receiversIds.Concat(senderIds).Distinct().ToList();

        Console.WriteLine($"\nDo you want to access user? <y>");
        var access=Console.ReadLine();

        if (access != "y")
        {
            PrintMessage("Going back to home page...", ResponseResultType.Success);
            return new HomePageAction { UserId=UserId };
        }
        var index = 0;
        foreach(var item in everyId)
        {
            Console.WriteLine($"{++index}.{userRepository.GetById(item).Email}");
        }
     
        Console.WriteLine("Enter number you want to change ");
        var input = Checker.NumberInput(maxNumber: index);

        if (!markedSpam.Contains(everyId[input - 1]))
        {
            //Console.WriteLine("User is not spam. Mark it as spam? <y>");//napravi f-ju
            spamRepository.MarkSpam(UserId, userRepository.GetById(everyId[input - 1]).Id);
            PrintMessage("Successfully marked as spam!\nGoing back to main menu ", ResponseResultType.Success);
            return new HomePageAction { UserId = UserId };
        }
        else if(markedSpam.Contains(everyId[input - 1]))
        {
            spamRepository.RemoveSpam(UserId, userRepository.GetById(everyId[input - 1]).Id);
            PrintMessage("Successfully  removed spam!\nGoing back to main menu", ResponseResultType.Success);
        }

        return new HomePageAction { UserId=UserId};
    }
}
