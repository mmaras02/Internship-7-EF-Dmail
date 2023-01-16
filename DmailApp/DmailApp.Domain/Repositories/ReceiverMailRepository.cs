using DmailApp.Data.Entities;
using DmailApp.Data.Entities.Models;
using DmailApp.Domain.Enums;

namespace DmailApp.Domain.Repositories;

public class ReceiverMailRepository : BaseRepository
{
    public ReceiverMailRepository(DmailAppDbContext dbContext) : base(dbContext) { }

    public ResponseResultType Add(ReceiverMail receiverMail)
    {
        DbContext.Recipients.Add(receiverMail);

        return SaveChanges();
    }
}
