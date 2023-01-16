using DmailApp.Domain.Factories;
using DmailApp.Domain.Repositories;
using DmailApp.Presentation.Entities.Interfaces;

namespace DmailApp.Presentation.Entities.Actions;

public class HomePageAction:IAction
{
    public int UserId;

    public IAction Action()
    {
        var userRepository = RepositoryFactory.Create<UserRepository>();

        Console.Clear();
        PrintTitle("Home page");
        PrintHomePageMenu();

        switch (NumberInput(maxNumber: 7))
        {
            case 1:
                return new PrimaryMailAction { UserId = UserId };

            case 2:
                return new OutgoingMailAction { UserId = UserId };

            case 3:
                return new SpamMailAction { UserId = UserId };

            case 4:
                return new SendMailAction { UserId = UserId };

            case 5:
                return new SendEventAction { UserId = UserId };

            case 6:
                return new ProfileSettingsAction { UserId = UserId };

            case 7:
                return new LogOutAction { UserId = UserId };
        }
        return null;
    }
}
