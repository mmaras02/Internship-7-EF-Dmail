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
        DateTime loginTime = new DateTime();
        //var email = "";
        //var password = "";

        var userRepository = RepositoryFactory.Create<UserRepository>();

        Console.Clear();
        Printer.PrintTitle("Login");

        Console.WriteLine("Enter your email");
        var email = Checker.CheckEmail(input => userRepository.DoesEmailExists(input));

        Console.WriteLine("Enter password");
        var password = Checker.PasswordInput(input => userRepository.CheckPassword(email, input));
       
        var userId =userRepository.GetIdByEmail(email);

        if (userRepository.CheckLogin(email, password) == ResponseResultType.Success)
            return new HomePageAction { UserId = userId };

        //if(userRepository.CheckLogin(email,password)==ResponseResultType.Error)
        //{
        //    Console.WriteLine("Login unsuccessfull. Try again in 30 seconds!");
        //    Thread.Sleep(30000);
        //    Console.ReadLine();
        //}
        return new HomePageAction { };
    }
}
