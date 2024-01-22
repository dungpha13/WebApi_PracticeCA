namespace PracticeCA.Domain;

public interface IUserRepository : IEFRepository<User, User>
{
    Task<User?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<List<User>> FindByIdsAsync(Guid[] ids, CancellationToken cancellationToken = default);

    // Task<User?> FindByUsernameAndPassword(string username, string password, CancellationToken cancellationToken = default);
}
