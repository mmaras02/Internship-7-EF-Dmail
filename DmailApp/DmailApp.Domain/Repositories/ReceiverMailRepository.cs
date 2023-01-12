using DmailApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmailApp.Domain.Repositories;

public class ReceiverMailRepository:BaseRepository
{
    public ReceiverMailRepository(DmailAppDbContext dbContext) : base(dbContext) { }

}
