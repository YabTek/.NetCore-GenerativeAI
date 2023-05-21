using Microsoft.AspNetCore.Identity;

namespace TaskTracker.Identity.Models;

public class AppUser : IdentityUser
{
    public string Fullname { get; set; } = "";

}
