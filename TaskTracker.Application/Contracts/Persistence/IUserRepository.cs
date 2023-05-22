using TaskTracker.Domain;

namespace TaskTracker.Application.Contracts.Persistence;

public interface IUserRepository : IGenericRepository<User>
{
      Task<User> GetUserWithDetails(int id, bool includeTask);

}
