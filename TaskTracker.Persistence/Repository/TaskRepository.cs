using Microsoft.EntityFrameworkCore;
using TaskTracker.Application.Contracts.Persistence;
using TaskTracker.Domain;

namespace TaskTracker.Persistence.Repository;


 public class TaskRepository : GenericRepository<task>, ITaskRepository
    {
        private readonly TaskTrackerDbContext _dbContext;

        public TaskRepository(TaskTrackerDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

         public async Task<task> GetTaskWithDetails(int id, bool includeUser, bool includeChecklists)
        {
            IQueryable<task> query = _dbContext.Set<task>();

            if (includeUser)
            {
                query = query.Include(t => t.User);
            }

            if (includeChecklists)
            {
                query = query.Include(t => t.Checklists);
            }

            return await query.FirstOrDefaultAsync(t => t.Id == id);
        }
        
    }
    
