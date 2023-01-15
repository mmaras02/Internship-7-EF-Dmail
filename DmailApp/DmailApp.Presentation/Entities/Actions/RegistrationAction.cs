using DmailApp.Domain.Repositories;
using DmailApp.Presentation.Entities.Interfaces;
using DmailApp.Presentation.Helpers;
using System.Security.Cryptography.X509Certificates;
using DmailApp.Domain.Factories;
using DmailApp.Domain.Enums;

namespace DmailApp.Presentation.Entities.Actions;

public class RegistrationAction:IAction
{
    public IAction Action()
    {
        var userRepository = RepositoryFactory.Create<UserRepository>();

        PrintTitle("Register");

        Console.WriteLine("Enter your email: ");
        var email = Checker.CheckEmail(input => !userRepository.DoesEmailExists((string)input));

        if (userRepository.ValidateEmail(email) == ResponseResultType.ValidationError)
        {
            PrintMessage("User with same email already exists!", ResponseResultType.Warning);
            return new MainMenuAction { };
        }

        Console.WriteLine("Enter password: ");
        var password = Checker.PasswordInput(input=>true);

        Console.WriteLine("Enter password again ");
        var confirmPassword=Checker.PasswordInput(input=>true);

        if (password != confirmPassword)
        {
            Console.WriteLine("Wrong input! Passwords do not match!");
            return new MainMenuAction { };
        }
        var (userId, status) = userRepository.Add(email, password);

        if (status == ResponseResultType.Success)
            return new HomePageAction { UserId = userId };

        return null;
    }
}
