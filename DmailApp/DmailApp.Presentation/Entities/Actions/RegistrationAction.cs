using DmailApp.Domain.Repositories;
using DmailApp.Presentation.Entities.Interfaces;
//using System.Security.Cryptography.X509Certificates;
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
        var email = CheckEmail(input => !userRepository.DoesEmailExists((string)input));

        if (userRepository.ValidateEmail(email) == ResponseResultType.ValidationError)
        {
            PrintMessage("User with same email already exists!", ResponseResultType.Warning);
            return new MainMenuAction { };
        }

        Console.WriteLine("Enter password: ");
        var password = PasswordInput(input=>true);

        Console.WriteLine("Enter password again ");
        var confirmPassword=PasswordInput(input=>true);

        if (password != confirmPassword)
        {
            PrintMessage("Wrong input! Passwords do not match!", ResponseResultType.Warning);
            return new MainMenuAction { };
        }
        
        var captcha = GenerateRandomString();
        var userCaptha=Console.ReadLine();
        if(captcha!=userCaptha)
        {
            PrintMessage("Incorrect string input!",ResponseResultType.Error);
            return new MainMenuAction { };
        }
        var (userId, status) = userRepository.Add(email, password);

        if (status == ResponseResultType.Success)
            return new HomePageAction { UserId = userId };

        return null;
    }
    public string GenerateRandomString()
    {
        int length = 8;
        const string validCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        var random = new Random();
        var result = new string(
            Enumerable.Repeat(validCharacters, length)
                      .Select(s => s[random.Next(s.Length)])
                      .ToArray());

        Console.WriteLine("Repeat the given string: ");
        Console.WriteLine(result);
        return result;
    }
}
