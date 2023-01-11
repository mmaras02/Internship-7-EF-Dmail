using DmailApp.Presentation.Entities.Interfaces;
using DmailApp.Presentation.Helpers;
using System;

namespace DmailApp.Presentation.Entities.Actions;

public class MainMenuAction : IAction
{
    public IAction Action()
    {
        Console.Clear();
        Printer.PrintTitle("Welcome to DmailApp");
        Printer.PrintMainMenu();

        switch(Checker.NumberInput(2))
        {
            case 1:
                return new LoginAction { };

            case 2:
                return new RegistrationAction { };
        }
        return null;

    }

}
