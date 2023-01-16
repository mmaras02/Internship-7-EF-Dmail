using DmailApp.Data.Entities.Enums;

namespace DmailApp.Data.Entities.Models;
public class ReceiverMail
{//user that received
    public int MailId { get; set; }
    public Mail Mail { get; set; } = null!;

    public int ReceiverId { get; set; }
    public User Receiver { get; set; } = null!;

    public MailStatus? MailStatus { get; set; } = null!;
    public EventStatus? EventStatus { get; set; } = null!;

}

