using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;


namespace TaskTracker.Identity;

public class TaskTrackerIdentityDbContextFactory : IDesignTimeDbContextFactory<TaskTrackerIdentityDbContext>
{
    public TaskTrackerIdentityDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory() + "../../TaskTracker.Presentation")
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<TaskTrackerIdentityDbContext>();
        var connectionString = configuration.GetConnectionString("TaskTrackerIdentityConnectionString");

        builder.UseNpgsql(connectionString);

        return new TaskTrackerIdentityDbContext(builder.Options);
    }
}
