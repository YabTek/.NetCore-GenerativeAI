using TaskTracker.Domain;

namespace TaskTracker.Application.Contracts.Persistence;

public interface ITaskRepository : IGenericRepository<task>
{
      Task<task> GetTaskWithDetails(int id, bool includeChecklists);

}
