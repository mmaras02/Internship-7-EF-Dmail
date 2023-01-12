using DmailApp.Presentation.Entities.Interfaces;

namespace DmailApp.Presentation.Entities.Actions;

public class LogOutAction : IAction
{
    public int UserId;
    public IAction Action()
    {
        return new MainMenuAction { };
    }
}
