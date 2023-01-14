using DmailApp.Data.Entities.Models;
using DmailApp.Domain.Enums;
using DmailApp.Domain.Factories;
using DmailApp.Domain.Repositories;
using DmailApp.Presentation.Entities.Interfaces;
using DmailApp.Presentation.Helpers;


namespace DmailApp.Presentation.Entities.Actions;

public class SpamMailAction : IAction
{
    public int UserId;
    public IAction Action()
    {
        Console.Clear();

        var spamRepository = RepositoryFactory.Create<SpamRepository>();
        var userRepository= RepositoryFactory.Create<UserRepository>();

        if (Printer.PrintSpamMail(UserId) == ResponseResultType.Error)
            return new HomePageAction { UserId=UserId};

        Console.WriteLine($"\n\n1.Mark new spam user\n2.Remove spam user\n'exit'-exit\n");

        switch(Checker.NumberInput(maxNumber:2))
        {
            case 1:
                Console.WriteLine("Enter email you want to mark as spam: ");
                var email = Checker.CheckEmail(input => userRepository.DoesEmailExists(input));

                spamRepository.MarkSpam(UserId, userRepository.GetByEmail(email).Id);
                Printer.ConfirmMessage("Sucessfully added spam user", ResponseResultType.Success);

                return new SpamMailAction{UserId=UserId };
            case 2:
                Console.WriteLine("Enter email you want to remove from spam list");
                var email1 = Console.ReadLine();

                spamRepository.RemoveSpam(UserId, userRepository.GetByEmail(email1).Id);
                Printer.ConfirmMessage("Sucessfully removed spam", ResponseResultType.Success);

                return new SpamMailAction { UserId = UserId };

            default:
                Printer.ConfirmMessage("Wrong input", ResponseResultType.Error);
                break;
        }
      
        return new HomePageAction{UserId=UserId };
    }
}
