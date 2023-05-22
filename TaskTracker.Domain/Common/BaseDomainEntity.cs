namespace TaskTracker.Domain.Common;

public class BaseDomainEntity
{
    public int Id {get; set;}
    public DateTime Start_date { get; set; }
    public DateTime End_date { get; set; }
    public Status Status {get; set;}

}

public enum Status
{
    Completed,
    NotCompleted
}