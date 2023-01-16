using DmailApp.Data.Entities.Enums;
using DmailApp.Data.Entities.Models;
using DmailApp.Domain.Enums;
using DmailApp.Domain.Factories;
using DmailApp.Domain.Repositories;

namespace DmailApp.Presentation.Helpers;

public static class Printer
{
    public static UserRepository? userRepository = RepositoryFactory.Create<UserRepository>();
    public static MailRepository? mailRepository = RepositoryFactory.Create<MailRepository>();
    public static SpamRepository? spamRepository = RepositoryFactory.Create<SpamRepository>();
    public static void PrintMessage(string message, ResponseResultType messageType)
    {
        Console.ForegroundColor = ConsoleColor.Black;
        Console.BackgroundColor = ConsoleColor.White;

        if (messageType == ResponseResultType.Error)
            Console.BackgroundColor = ConsoleColor.Red;

        if (messageType == ResponseResultType.Success)
            Console.BackgroundColor = ConsoleColor.Green;

        if (messageType == ResponseResultType.NoChanges)
            Console.BackgroundColor = ConsoleColor.Yellow;

        if (messageType == ResponseResultType.Warning)
            Console.BackgroundColor = ConsoleColor.DarkRed;

        Console.WriteLine($"{message} press any key to continue");
        Console.ReadKey();
        Console.ResetColor();
        Console.Clear();
    }
    public static void PrintTitle(string message)
    {
        Console.Clear();
        Console.BackgroundColor = ConsoleColor.Blue;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine(new String('-', 30));
        Console.WriteLine($"    {message}       ");
        Console.WriteLine(new String('-', 30));
        Console.ResetColor();
    }
    public static void PrintMainMenu()
    {
        Console.WriteLine("Actions available:\n1 - Login\n2 - Registration\n'exit'.Exit the app\n");
    }
    public static void PrintHomePageMenu()
    {
        PrintTitle("Home page");
        Console.WriteLine("Actions available\n1.Primary mail\n2.Outgoing mail\n3.Spam mail\n4.Send new mail\n5.Send new event\n6.Profile settings\n7.Log out\n0.Exit the app");
    }
    public static void PrintInbox()
    {
        Console.WriteLine("Actions available\n1.Read mail\n2.Unread mail\n3.Search mail from user\n0.Back to main menu\n");
    }
    public static void PrintOptions()
    {
        Console.WriteLine("\nYour options for choosen mail:");
        Console.WriteLine("1.Mark as unread\n2.Mark as spam\n3.Delete mail\n4.Reply to mail\n0.Go back to primary mail");
    }
    public static void ReadMail(int userId, List<Mail> mail, bool inbox)
    {
        var index = 0;
        foreach (var item in mail)
        {
            Console.WriteLine($"{++index}. {item.Title} - {userRepository.GetById(item.SenderId).Email}");
        }
        if (index is 0)
        {
            PrintMessage("You don't have any mails in this container!", ResponseResultType.NoChanges);
            return;
        }
        FilterMail(userId, index, mail, inbox);
    }
    public static void FilterMail(int userId, int index, List<Mail> mail, bool inbox)
    {
        Console.WriteLine("\nNumber of the mail you want to filter\n0.Go back to main");
        int.TryParse(Console.ReadLine(), out int input);

        Console.Clear();
        if (input is 0)
            return;

        if (input < 0 && input >= index)
            PrintMessage("Incorrect input! ", ResponseResultType.Error);

        PrintDetailedMail(mail[input - 1].MailId, userId);
        var message = mailRepository.GetById(mail[input - 1].MailId);

        if (!inbox)
        {
            GetOutboxActions(message.MailId);
        }
        else
            GetInboxActions(message, userId);
    }
    public static void GetOutboxActions(int mailId)
    {
        if (!GetConfirmation("delete this mail? "))
            return;

        mailRepository.Delete(mailId);
    }
    public static void GetInboxActions(Mail mail, int userId)
    {
        PrintOptions();
        switch (NumberInput(maxNumber: 4))
        {
            case 1:
                mailRepository.ChangeToUnread(mail);
                PrintMessage("You changed mail to unread! ", ResponseResultType.Success);
                return;
            case 2:
                if (spamRepository.MarkSpam(userId, mail.SenderId) == ResponseResultType.NoChanges)
                    PrintMessage("User already marked as spam!", ResponseResultType.NoChanges);
                else
                    PrintMessage("Sucessfully marked spam! ", ResponseResultType.Success);
                return;
            case 3:
                mailRepository.Delete(mail.MailId);
                PrintMessage("You deleted choosen mail! ", ResponseResultType.Success);
                return;
            case 4:
                //odgovori na event
                if (mail.MailType == MailType.EventMail)
                {
                    Console.WriteLine("Answer the event invitation:");
                    Console.WriteLine("1.Accept invitation\n2.Decline invitation\n0.Leave unanswered");

                    var input = NumberInput(maxNumber: 2);
                    if (!GetConfirmation("take this action?"))
                        return;

                    switch (input)
                    {
                        case 1:
                            mailRepository.ChangeEventStatus(mail, EventStatus.Accepted);
                            PrintMessage("Invitation accepted!", ResponseResultType.Success);
                            break;
                        case 2:
                            mailRepository.ChangeEventStatus(mail, EventStatus.Rejected);
                            PrintMessage("Invitation rejected!", ResponseResultType.Success);
                            break;
                        case 0:
                            PrintMessage("Left unanswered!", ResponseResultType.Warning);
                            return;
                        default:
                            return;
                    }
                }
                if (mail.MailType == MailType.MessageMail)
                {
                    if (mailRepository.NewMail(userId, mail.SenderId) == ResponseResultType.Error)
                    {
                        PrintMessage("Action stopped!\nReturning... ", ResponseResultType.ValidationError);
                        return;
                    }
                    PrintMessage("Mail sent successfully!", ResponseResultType.Success);
                    return;
                }
                break;
            default:
                return;
        }
    }
    public static void PrintDetailedMail(int mailId, int userId)
    {
        mailRepository.ChangeToRead(mailId);

        Mail mail = mailRepository.GetById(mailId);
        var sender = userRepository.GetById(mail.SenderId);

        if (mail.MailType is MailType.MessageMail)
        {
            Console.WriteLine($"Title: {mail.Title}\nTime of sending: {mail.TimeOfSending}\nSender: {sender.Email}");
            Console.WriteLine("-------------------------");
            Console.WriteLine($"{mail.Content}\n");
        }
        else if (mail.MailType is MailType.EventMail)
        {
            Console.WriteLine($"Title: {mail.Title}\nTime of sending: {mail.TimeOfSending}\nSender: {sender.Email}\n");
            Console.WriteLine($"Event start: {mail.EventTime}\nEvent duration: {mail.EventDuration}");

            Console.WriteLine("\nList of  other invites");
            var recipients = mailRepository.GetRecipients(mail.MailId);

            foreach (var recipient in recipients)
            {
                Console.WriteLine($"{recipient.Email}");
            }
            Console.WriteLine($"\nYou have {mailRepository.GetEventStatus(mailId)} this invitation!");
        }
    }
    public static void PrintUsers(List<int> userIds, List<int> markedSpam)
    {
        var index = 0;
        foreach (var item in userIds)
        {
            if (markedSpam.Contains(item))
                Console.WriteLine($"{++index}.{userRepository.GetById(item).Email} (marked as spam)");
            else
                Console.WriteLine($"{++index}.{userRepository.GetById(item).Email}");
        }
    }
}
