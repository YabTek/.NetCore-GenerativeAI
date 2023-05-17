namespace TaskTracker.Domain;

public class Task
{
    public int Id { get; set; }
    public int Owner { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Start_date { get; set; }
    public DateTime End_date { get; set; }
    public StatusEnum Status {get; set;}

}

public enum StatusEnum
{
    Completed,
    NotCompleted
}
