using System.ComponentModel.DataAnnotations;

namespace TaskTracker.Application.Models;

public class RegistrationRequest
{
    [Required]
    [EmailAddress]
    public string Email {get; set;} = "";

    [Required]
    public string Fullname {get; set;} = "";

    [Required]
    public string UserName {get; set;} = "";

    [Required]
    public string Password{get; set;} = "";

}
