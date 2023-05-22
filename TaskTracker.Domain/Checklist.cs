using TaskTracker.Domain.Common;

namespace TaskTracker.Domain;

public class Checklist : BaseDomainEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int associated_task { get; set; }
    public task Task { get; set; }

}  


