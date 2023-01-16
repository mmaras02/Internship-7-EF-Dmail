using DmailApp.Data.Entities;
using DmailApp.Domain.Enums;

namespace DmailApp.Domain.Repositories;
public abstract class BaseRepository
{
    protected readonly DmailAppDbContext DbContext;

    protected BaseRepository(DmailAppDbContext dbContext)
    {
        DbContext = dbContext;
    }
    protected ResponseResultType SaveChanges()
    {
        var changes = DbContext.SaveChanges() > 0;
        if (!changes)
            return ResponseResultType.NoChanges;

        return ResponseResultType.Success;
    }
}
