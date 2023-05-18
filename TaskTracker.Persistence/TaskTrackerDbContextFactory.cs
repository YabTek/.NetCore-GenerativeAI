using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TaskTracker.Persistence;

    public class TaskTrackerDbContextFactory : IDesignTimeDbContextFactory<TaskTrackerDbContext>
    {
        public TaskTrackerDbContext CreateDbContext(string[] args)
                {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory() + "../../TaskTracker.Presentation")
                 .AddJsonFile("appsettings.json")
                 .Build();

            var builder = new DbContextOptionsBuilder<TaskTrackerDbContext>();
            var connectionString = configuration.GetConnectionString("TaskTrackerConnectionString");

            builder.UseNpgsql(connectionString);

            return new TaskTrackerDbContext(builder.Options);
        }
        
    }


