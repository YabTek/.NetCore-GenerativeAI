using TaskTracker.Application.DTOs.Common;

namespace TaskTracker.Application.DTOs.Tasks;

public class UpdateTaskDto : BaseDto,ITaskDto
{
        public int Owner { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        
}
