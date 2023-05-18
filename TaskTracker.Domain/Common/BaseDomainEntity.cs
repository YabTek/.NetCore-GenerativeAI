namespace TaskTracker.Domain.Common;

public class BaseDomainEntity
{
    public int Id { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime LastModifiedDate { get; set; }
}
