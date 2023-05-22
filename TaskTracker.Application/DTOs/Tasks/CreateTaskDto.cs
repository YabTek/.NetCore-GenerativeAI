using TaskTracker.Application.DTOs.Common;
using TaskTracker.Domain.Common;

namespace TaskTracker.Application.DTOs.Tasks;

public class CreateTaskDto : BaseDto,ITaskDto
{
        public int Owner { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
}
