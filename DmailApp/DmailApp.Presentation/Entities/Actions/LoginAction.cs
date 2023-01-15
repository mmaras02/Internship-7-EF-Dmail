using DmailApp.Domain.Repositories;
using DmailApp.Presentation.Entities.Interfaces;
using DmailApp.Presentation.Helpers;
using DmailApp.Domain.Factories;
using DmailApp.Domain.Enums;

namespace DmailApp.Presentation.Entities.Actions;

public class LoginAction:IAction
{
    public IAction Action ()
    {
        var userRepository = RepositoryFactory.Create<UserRepository>();

        PrintTitle("Login");

        Console.WriteLine("Enter your email");
        var email=Console.ReadLine();

        Console.WriteLine("Enter password");
        var password = ReadPassword();

        if (userRepository.CheckLogin(email, password) == ResponseResultType.Success)
        {
            var userId = userRepository.GetIdByEmail(email);
            return new HomePageAction { UserId = userId };
        }

        PrintMessage("Incorrect email-password combination\n30 seconds timeout!",ResponseResultType.Warning);
        Thread.Sleep(30000);
        return new MainMenuAction { };
    }
}
