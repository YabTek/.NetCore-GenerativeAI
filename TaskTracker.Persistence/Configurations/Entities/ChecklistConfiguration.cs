using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Domain;

namespace TaskTracker.Persistence.Configurations.Entities;
 public class ChecklistConfiguration : IEntityTypeConfiguration<Checklist>
    {
        public void Configure(EntityTypeBuilder<Checklist> builder)
        {
            builder.HasData(
                new Checklist
                {
                    Id = 1,
                    Title = "create meeting agenda",
                    Description = "checklist for a meeting agenda",
                    associated_task = 3,
                    Status = ChecklistStatus.Completed
                    
                },

                new Checklist
                {
                    Id = 2,
                    Title = "pack things",
                    Description = "checklist for packing things",
                    associated_task = 4,
                    Status = ChecklistStatus.Completed
                    
                }
                );
        }

    }

