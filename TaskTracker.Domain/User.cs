using TaskTracker.Domain.Common;

namespace TaskTracker.Domain;

public class User 
{
    public User()
    {
       Tasks = new HashSet<task>();
    }
    public int Id { get; set; }
    public string Fullname { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public virtual ICollection<task> Tasks { get; set; }


}
