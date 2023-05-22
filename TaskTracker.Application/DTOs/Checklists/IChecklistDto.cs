using TaskTracker.Domain;
using TaskTracker.Domain.Common;

namespace TaskTracker.Application.DTOs.Checklists;

public interface IChecklistDto
{
      public  string Title { get; set; }
      public  string Description { get; set; }
      public  int associated_task { get; set; }
      public DateTime Start_date { get; set; }
      public DateTime End_date { get; set; }
      public Status Status { get; set; }
}
