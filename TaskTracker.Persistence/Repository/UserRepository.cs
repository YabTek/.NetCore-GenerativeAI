using TaskTracker.Application.Contracts.Persistence;
using TaskTracker.Domain;

namespace TaskTracker.Persistence.Repository;

    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly TaskTrackerDbContext _dbContext;

        public UserRepository(TaskTrackerDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }

