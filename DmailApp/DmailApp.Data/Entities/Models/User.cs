namespace DmailApp.Data.Entities.Models;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string? Password { get; set; }
    public byte[] HashedPassword { get; set; }
    public DateTime FailedLogin { get; set; }

    public ICollection<Mail> SentMail { get; } = new List<Mail>();
    public ICollection<ReceiverMail> ReceivedMail { get; } = new List<ReceiverMail>();
    public ICollection<SpamFlag> SpamUsers { get; } = new List<SpamFlag>();

}
