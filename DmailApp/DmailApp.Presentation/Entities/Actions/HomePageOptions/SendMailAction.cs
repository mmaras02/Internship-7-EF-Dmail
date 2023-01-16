using DmailApp.Domain.Enums;
using DmailApp.Domain.Factories;
using DmailApp.Domain.Repositories;
using DmailApp.Presentation.Entities.Interfaces;

namespace DmailApp.Presentation.Entities.Actions;

public class SendMailAction : IAction
{
    public int UserId;
    public IAction Action()
    {
        PrintTitle("New mail");
        var userRepository = RepositoryFactory.Create<UserRepository>();
        var mailRepository = RepositoryFactory.Create<MailRepository>();
        var receiverMailRepository = RepositoryFactory.Create<ReceiverMailRepository>();

        Console.WriteLine("Enter receivers emails (separate with <, >):");
        string[] emails = Console.ReadLine().Split(", ");
        var receiversIds = 0;
        foreach (string email in emails)
        {
            if (!userRepository.DoesEmailExists(email))
                PrintMessage($"{email} doesn't exist! ", ResponseResultType.Warning);
            receiversIds = userRepository.GetIdByEmail(email);
        }

        if (mailRepository.NewMail(UserId, receiversIds) == ResponseResultType.Error)
            PrintMessage("Sending stopped!", ResponseResultType.Error);

        PrintMessage("Message sent successfuly!", ResponseResultType.Success);

        return new HomePageAction { UserId = UserId };
    }
}
