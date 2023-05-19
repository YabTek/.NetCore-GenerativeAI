using TaskTracker.Domain;

namespace TaskTracker.Application.DTOs.Checklists;

public class CreateChecklistDto : IChecklistDto
{
     public   string Title { get; set; }
     public   string Description { get; set; }
     public   int associated_task { get; set; }
     public  ChecklistStatus Status { get; set; }
}
