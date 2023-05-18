using TaskTracker.Application.DTOs.Common;

namespace TaskTracker.Application.DTOs.Tasks;

public class TaskDto : BaseDto,ITaskDto
{
        public int Owner { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Start_date { get; set; }
        public DateTime End_date { get; set; }
        public TaskStatus Status { get; set; }
}
