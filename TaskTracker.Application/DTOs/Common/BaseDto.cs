using TaskTracker.Domain.Common;

namespace TaskTracker.Application.DTOs.Common;

public class BaseDto
{
    public int Id { get; set; }
    public DateTime Start_date { get; set; }
    public DateTime End_date { get; set; }
    public Status Status { get; set; }
}
