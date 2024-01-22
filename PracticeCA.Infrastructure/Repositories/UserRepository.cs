using AutoMapper;
using PracticeCA.Domain;

namespace PracticeCA.Infrastructure;

public class UserRepository : RepositoryBase<User, User, ApplicationDbContext>, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public async Task<User?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await FindAsync(x => x.Id == id, cancellationToken);
    }

    public Task<List<User>> FindByIdsAsync(Guid[] ids, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    // public async Task<User?> FindByUsernameAndPassword(string username, string password, CancellationToken cancellationToken = default)
    // {
    //     return await FindAsync(x => x.Username == username && x.Password == password, cancellationToken);
    // }
}
