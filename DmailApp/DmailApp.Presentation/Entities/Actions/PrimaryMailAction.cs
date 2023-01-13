using DmailApp.Data.Entities.Models;
using DmailApp.Domain.Enums;
using DmailApp.Domain.Factories;
using DmailApp.Domain.Repositories;
using DmailApp.Presentation.Entities.Interfaces;
using DmailApp.Presentation.Helpers;

namespace DmailApp.Presentation.Entities.Actions;

public class PrimaryMailAction : IAction
{
    public int UserId;
    public IAction Action()
    {
        Printer.PrintInbox();
        var userRepository = RepositoryFactory.Create<UserRepository>();
        var mailRepository=RepositoryFactory.Create<MailRepository>();

        switch(Checker.NumberInput(maxNumber: 3))
        {
            case 1:
                var readMail = mailRepository.GetReadMail(UserId);
                Printer.ReadMail(UserId,readMail);
                Checker.UserInput("go back to primary mail");

                return new PrimaryMailAction { UserId = UserId }; 
            case 2:
                var readMails = mailRepository.GetUnreadMail(UserId);
                Printer.ReadMail(UserId, readMails);
                Checker.UserInput("go back to primary mail");

                return new PrimaryMailAction { UserId = UserId };
            case 3:
                break;
            case 0:
                return new HomePageAction { UserId = UserId };
            default:
                break;
        }
        return null;
    }
}
