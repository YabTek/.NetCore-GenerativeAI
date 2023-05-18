using TaskTracker.Domain;

namespace TaskTracker.Application.Contracts.Persistence;

public interface ITaskRepository : IGenericRepository<task>
{
}
