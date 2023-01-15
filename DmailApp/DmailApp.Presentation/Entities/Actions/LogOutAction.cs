using DmailApp.Domain.Enums;
using DmailApp.Presentation.Entities.Interfaces;
using DmailApp.Presentation.Helpers;

namespace DmailApp.Presentation.Entities.Actions;

public class LogOutAction : IAction
{
    public int UserId;
    public IAction Action()
    {
        PrintMessage("Logging out...", ResponseResultType.Success);
        return new MainMenuAction { };
    }
}
