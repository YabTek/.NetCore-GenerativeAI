
namespace TaskTracker.Application.DTOs.Users;

public class CreateUserDto : IUserDto
{
    public string Fullname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

}
