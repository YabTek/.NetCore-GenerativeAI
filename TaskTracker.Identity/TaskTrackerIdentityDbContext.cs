using TaskTracker.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TaskTracker.Identity;

public class TaskTrackerIdentityDbContext : IdentityDbContext<AppUser>
{
    public TaskTrackerIdentityDbContext(DbContextOptions<TaskTrackerIdentityDbContext> options) :
        base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

    }
}