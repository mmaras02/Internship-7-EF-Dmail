using DmailApp.Data.Entities.Models;
using DmailApp.Domain.Factories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DmailApp.Presentation.Helpers;

public static class Checker
{
    public static int? NumberInput(int maxNumber)
    {
        while(true)
        {
            Console.WriteLine("Enter your input: ");
            var input = Console.ReadLine();

            if (input == "exit")
                return null;

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
    public static bool CheckForNumber(string suspectString, out int number)
    {
        if (int.TryParse(suspectString, out int result))
        {
            number = result;
            return true;
        }

        number = 0;
        return false;
    }
    private static string TextInput(Func<string, bool> valid,Func<string>read)
    {
        while(true)
        {
            Console.WriteLine("Enter input here: ");
            var input=read();

            if(valid(input))
                return input;
        }
        Console.WriteLine("wrong input! Try again");
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
    

}
