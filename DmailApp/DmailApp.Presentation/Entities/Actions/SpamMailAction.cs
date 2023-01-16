using DmailApp.Domain.Enums;
using DmailApp.Domain.Factories;
using DmailApp.Domain.Repositories;
using DmailApp.Presentation.Entities.Interfaces;

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

        switch (NumberInput(maxNumber: 3))
        {
            case 1:
                var readSpam = spamRepository.GetReadSpamMail(UserId);
                ReadMail(UserId, readSpam, true);

                return new SpamMailAction { UserId = UserId };
            case 2:
                var unreadSpam = spamRepository.GetUnreadSpamMail(UserId);
                ReadMail(UserId, unreadSpam, true);

                return new SpamMailAction { UserId = UserId };
            case 3:
                CheckSearchedInput(UserId);

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
