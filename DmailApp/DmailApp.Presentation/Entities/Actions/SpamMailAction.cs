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
        PrintTitle("Spam page");
        PrintInbox();

        var spamRepository = RepositoryFactory.Create<SpamRepository>();
        var userRepository= RepositoryFactory.Create<UserRepository>();

        switch (Checker.NumberInput(maxNumber: 3))
        {
            case 1:
                var readSpam = mailRepository.GetReadSpamMail(UserId);
                ReadMail(UserId, readSpam, true);

                return new SpamMailAction { UserId = UserId };
            case 2:
                var unreadSpam = mailRepository.GetUnreadSpamMail(UserId);
                ReadMail(UserId, unreadSpam, true);

                return new SpamMailAction { UserId = UserId };
            case 3:
                Console.Write("Search specific user: ");
                var query=Console.ReadLine();

                var result=mailRepository.SearchByString(UserId, query);
                if(result is null)
                {
                    PrintMessage("Nothing was found for your search!", ResponseResultType.Error);
                    return new SpamMailAction { UserId = UserId };    
                }
                Console.WriteLine("Here are found mails");
                ReadMail(UserId, result, true);

                return new SpamMailAction { UserId = UserId };
                break;
            case 0:
                return new HomePageAction { UserId = UserId };
            default:
                break;
        }
        return new HomePageAction { UserId = UserId };
    }
}
