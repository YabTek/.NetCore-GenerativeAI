using TaskTracker.Application.Contracts.Persistence;
using TaskTracker.Domain;

namespace TaskTracker.Persistence.Repository;

 public class ChecklistRepository : GenericRepository<Checklist>, IChecklistRepository
    {
        private readonly TaskTrackerDbContext _dbContext;

        public ChecklistRepository(TaskTrackerDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
