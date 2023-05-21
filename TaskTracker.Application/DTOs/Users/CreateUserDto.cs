
using TaskTracker.Application.DTOs.Common;

namespace TaskTracker.Application.DTOs.Users;

public class CreateUserDto : BaseDto,IUserDto
{
    public string Fullname { get; set; } = "";
    public string Email { get; set; }
    public string Password { get; set; }
    public string UserName {get; set;} = "";


}
