using TaskTracker.Domain;

namespace TaskTracker.Application.Contracts.Persistence;

public interface IChecklistRepository : IGenericRepository<Checklist>
{
       // Task<Checklist> GetChecklistWithDetails(int id,bool includeTask);

}
