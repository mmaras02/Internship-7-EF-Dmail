using DmailApp.Domain.Factories;
using DmailApp.Domain.Repositories;
using DmailApp.Presentation.Entities.Interfaces;

namespace DmailApp.Presentation.Entities.Actions;

public class PrimaryMailAction : IAction
{
    public int UserId;
    public IAction Action()
    {
        PrintTitle("Inbox Mail");
        PrintInbox();
        var mailRepository = RepositoryFactory.Create<MailRepository>();

        switch (NumberInput(maxNumber: 3))
        {
            case 1:
                Console.Clear();
                var readMail = mailRepository.GetReadMail(UserId);
                ReadMail(UserId, readMail, true);

                return new PrimaryMailAction { UserId = UserId };
            case 2:
                Console.Clear();
                var readMails = mailRepository.GetUnreadMail(UserId);
                ReadMail(UserId, readMails, true);

                return new PrimaryMailAction { UserId = UserId };
            case 3:
                Console.Clear();
                CheckSearchedInput(UserId);
                return new PrimaryMailAction { UserId = UserId };
                break;
            case 0:
                return new HomePageAction { UserId = UserId };
            default:
                break;
        }
        return null;
    }
}
