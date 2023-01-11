using DmailApp.Data.Entities.Enums;
using DmailApp.Data.Entities.Models;

namespace DmailApp.Data.Entities.Models;

public class SpamFlag
{
    public int UserId { get; set; }
    public User User { get; set; }

    public int SpamUserId { get; set; }
    public User SpamUser { get; set; }

}
