using TaskTracker.Domain.Common;

namespace TaskTracker.Domain;

public class User : BaseDomainEntity
{
    public string Fullname { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
   

}
