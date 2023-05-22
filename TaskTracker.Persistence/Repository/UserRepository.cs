using Microsoft.EntityFrameworkCore;
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
    public async Task<User> GetUserWithDetails(int id, bool includeTask)
        {
            IQueryable<User> query = _dbContext.Set<User>();

            if (includeTask)
            {
                query = query.Include(t => t.Tasks);
            }


            return await query.FirstOrDefaultAsync(t => t.Id == id);
        }
    }

