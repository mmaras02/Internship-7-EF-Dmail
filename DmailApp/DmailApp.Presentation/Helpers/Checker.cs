using DmailApp.Domain.Enums;

namespace DmailApp.Presentation.Helpers;

public static class Checker
{
    public static int NumberInput(int maxNumber)
    {
        while (true)
        {
            Console.WriteLine("Your input: ");
            var input = Console.ReadLine();

            if (input == "exit")
                return 0;

            var inputSuccess = int.TryParse(input, out int value);

            if (inputSuccess && value <= maxNumber)
                return value;

            Console.WriteLine("Invalid input! Try again!");
        }
    }
    public static bool CheckString(string suspectString, out string result)
    {
        if (string.IsNullOrWhiteSpace(suspectString) || suspectString.Length < 5)
        {
            result = string.Empty;
            return false;
        }
        result = suspectString;
        return true;
    }
    public static string TextInput(Func<string, bool> valid, Func<string> read)
    {
        while (true)
        {
            Console.WriteLine("Your input: ");
            var input = read();

            if (valid(input))
                return input;

            PrintMessage("wrong input! Try again! ", ResponseResultType.Error);
        }
    }
    public static string CheckEmail(Func<string, bool> valid) => TextInput(valid, Console.ReadLine);
    public static string PasswordInput(Func<string, bool> valid) => TextInput(valid, ReadPassword);
    public static string ReadPassword()
    {
        string password = "";
        ConsoleKeyInfo key;

        do
        {
            key = Console.ReadKey(true);

            if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                password += key.KeyChar;
                Console.Write("*");
            }
            else
            {
                if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b");
                }
            }
        }
        while (key.Key != ConsoleKey.Enter);

        Console.WriteLine();
        return password;
    }
    public static void UserInput(string message)
    {
        Console.WriteLine($"\nPress any key " + message);
        Console.ReadKey();
    }
    public static bool GetConfirmation(string message)
    {
        while (true)
        {
            Console.Write("\nAre you sure you want to " + message + "(y/n): ");
            string input = Console.ReadLine().Trim().ToLower();

            if (input == "y" || input == "yes") return true;
            if (input == "n" || input == "no") return false;

            PrintMessage("Invalid input! ", ResponseResultType.Error);
            UserInput("for return to main page");
        }
    }
    public static void CheckSearchedInput(int userId)
    {
        Console.Write("Search specific user: ");
        var query = Console.ReadLine();

        var result = mailRepository.SearchByString(userId, query);
        if (!result.Any())
        {
            PrintMessage("Nothing was found for your search!", ResponseResultType.Error);
            return;
        }
        Console.WriteLine("Here are found mails");
        ReadMail(userId, result, true);
    }
}
