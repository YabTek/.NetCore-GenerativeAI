namespace TaskTracker.Application.DTOs.Users;

public interface IUserDto
{
    public string Fullname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

}
