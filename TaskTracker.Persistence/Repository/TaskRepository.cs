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
    }
