using Microsoft.EntityFrameworkCore;
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
        //  public async Task<Checklist> GetChecklistWithDetails(int id, bool includeTask)
        // {
        //     IQueryable<Checklist> query = _dbContext.Set<Checklist>();

        //     if (includeTask)
        //     {
        //         query = query.Include(c => c.Task);
        //     }

        //     return await query.FirstOrDefaultAsync(c => c.Id == id);
            
        // }
    }
