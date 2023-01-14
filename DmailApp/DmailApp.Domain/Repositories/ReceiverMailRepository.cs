using DmailApp.Data.Entities;
using DmailApp.Data.Entities.Models;
using DmailApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmailApp.Domain.Repositories;

public class ReceiverMailRepository:BaseRepository
{
    public ReceiverMailRepository(DmailAppDbContext dbContext) : base(dbContext) { }

    public ResponseResultType Add(ReceiverMail receiverMail)
    {
        DbContext.Recipients.Add(receiverMail);

        return SaveChanges();
    }
}
