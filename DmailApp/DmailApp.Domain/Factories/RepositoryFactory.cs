using DmailApp.Domain.Factories;
using DmailApp.Domain.Repositories;

namespace TodoApp.Domain.Factories;

public class RepositoryFactory
{
    public static TRepository Create<TRepository>()
        where TRepository : BaseRepository
    {
        var dbContext = DbContextFactory.GetDmailAppDbContext();
        var repositoryInstance = Activator.CreateInstance(typeof(TRepository), dbContext) as TRepository;

        return repositoryInstance!;
    }
}