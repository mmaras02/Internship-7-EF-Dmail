using DmailApp.Presentation.Entities.Actions;
using DmailApp.Presentation.Entities.Interfaces;

namespace DmailApp.Presentation;

class Program
{
    static void Main(string[] args)
    {
        IAction action = new MainMenuAction { };
        while (action != null)
            action = action.Action();
    }
}