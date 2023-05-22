using TaskTracker.Application.DTOs.Common;
using TaskTracker.Application.DTOs.Tasks;

namespace TaskTracker.Application.DTOs.Users;

public class UserDto : IUserDto
{
    public int Id { get; set; }
    public string Fullname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public List<TaskDto> Tasks { get; set; } 


}
