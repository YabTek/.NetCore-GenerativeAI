using TaskTracker.Domain.Common;

namespace TaskTracker.Domain;


public class task : BaseDomainEntity
{
    public task()
    {
        Checklists = new HashSet<Checklist>();
    }
    public int Owner { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public User User { get; set; }
    public ICollection<Checklist> Checklists { get; set; }


}


