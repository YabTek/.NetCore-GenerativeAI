namespace TaskTracker.Domain;

public class Checklist
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int associated_task { get; set; }
    public StatusEnum Status {get; set;}

}  

public enum StatusEnum
{
    Completed,
    NotCompleted
}
