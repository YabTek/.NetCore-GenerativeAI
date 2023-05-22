using TaskTracker.Application.DTOs.Common;
using TaskTracker.Domain;
using TaskTracker.Domain.Common;

namespace TaskTracker.Application.DTOs.Checklists;

public class UpdateChecklistDto : BaseDto,IChecklistDto
{
        public string Title { get; set; }
        public string Description { get; set; }
        public int associated_task { get; set; }
}
