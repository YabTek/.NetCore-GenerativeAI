namespace TaskTracker.Application.DTOs.Users;

public class RegisterDto
{
    public string Fullname { get; set; }
    
    public string Email {get; set;}

    public string UserName {get; set;} = "";

    public string Password{get; set;}

}
