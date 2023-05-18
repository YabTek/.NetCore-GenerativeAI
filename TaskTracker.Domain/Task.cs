using TaskTracker.Domain.Common;

namespace TaskTracker.Domain;


public class task : BaseDomainEntity
{
    public int Owner { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Start_date { get; set; }
    public DateTime End_date { get; set; }
    public TaskStatus Status {get; set;}

}
public enum TaskStatus
{
    Completed,
    NotCompleted
}

