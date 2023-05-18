using TaskTracker.Domain;

namespace TaskTracker.Application.DTOs.Checklists;

public interface IChecklistDto
{
      public  string Title { get; set; }
      public  string Description { get; set; }
      public  int AssociatedTask { get; set; }
      public ChecklistStatus Status { get; set; }
}
