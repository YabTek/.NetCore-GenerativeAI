using TaskTracker.Application.DTOs.Common;
using TaskTracker.Domain;


namespace TaskTracker.Application.DTOs.Checklists;

public class UpdateChecklistDto : BaseDto,IChecklistDto
{
        public string Title { get; set; }
        public string Description { get; set; }
        public int AssociatedTask { get; set; }
        public ChecklistStatus Status { get; set; }
}
