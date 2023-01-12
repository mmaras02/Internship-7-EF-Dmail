using DmailApp.Data.Entities.Models;
using DmailApp.Domain.Factories;
using DmailApp.Domain.Repositories;
using DmailApp.Presentation.Entities.Interfaces;
using DmailApp.Presentation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DmailApp.Presentation.Entities.Actions;

public class SpamMailAction : IAction
{
    public int UserId;
    public IAction Action()
    {
        Console.Clear();
        var index = 0;
        var spamRepository = RepositoryFactory.Create<SpamRepository>();
        var userRepository= RepositoryFactory.Create<UserRepository>();

        Printer.PrintSpamMail(UserId);
        Console.WriteLine($"\n{++index}-Mark new spam user\n{++index}.Remove spam user\n'exit'-exit");

        switch(Checker.NumberInput(maxNumber:index))
        {
            case 1:
                Console.WriteLine("Enter email you want to mark as spam: ");
                var email = Checker.CheckEmail(input => userRepository.DoesEmailExists(input));
                var spamUserId=userRepository.GetByEmail(email).Id;

                spamRepository.MarkSpam(UserId,spamUserId);
                break;
            case 2:
                Console.WriteLine("Enter email you want to remove from spam");
                break;
            default:
                break;
        }
      
        return null;

    }
}
