using TaskTracker.Application.DTOs.Checklists;
using TaskTracker.Application.DTOs.Common;
using TaskTracker.Application.DTOs.Users;

namespace TaskTracker.Application.DTOs.Tasks;

public class TaskDto : BaseDto,ITaskDto
{
        public int Owner { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<ChecklistDto> Checklists { get; set; } 
       
}
