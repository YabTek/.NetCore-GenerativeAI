using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Domain;

namespace TaskTracker.Persistence.Configurations.Entities;

 public class TaskConfiguration : IEntityTypeConfiguration<task>
    {
        public void Configure(EntityTypeBuilder<task> builder)
        {
            builder.HasData(
                new task
                {
                    Id = 3,
                    Owner = 1,
                    Title = "Attend meeting",
                    Description = "This task is attending meeting",
                    Start_date = DateTime.Today,
                    End_date = DateTime.Now,
                    Status = Domain.TaskStatus.Completed  
                },

                new task
                {
                    Id = 4,
                    Owner = 1,
                    Title = "Attend class",
                    Description = "This task is attending class",
                    Start_date = DateTime.Today,
                    End_date = DateTime.Now,
                    Status = Domain.TaskStatus.Completed 
                    
                }
                );
        }

    }

