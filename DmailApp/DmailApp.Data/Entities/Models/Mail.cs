using DmailApp.Data.Entities.Enums;

namespace DmailApp.Data.Entities.Models;

public class Mail
{
    public Mail(string title)
    {
        Title = title;
    }
    public int MailId { get; set; }
    public string Title { get; set; }
    public DateTime TimeOfSending { get; set; }
    public MailType MailType { get; set; }
    public string? Content { get; set; } = null!;
    public DateTime? EventTime { get; set; } = null!;
    public TimeSpan? EventDuration { get; set; } = null!;


    public int SenderId { get; set; }
    public User Sender { get; set; }

    public ICollection<ReceiverMail> Recipients { get; set; } = new List<ReceiverMail>();
}
