using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Domain;

namespace TaskTracker.Persistence.Configurations.Entities;
 public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User
                {
                    Id = 1,
                    Fullname = "Abebe Kebede",
                    Email = "kebede@gmail.com",
                    Password = "abebe123@"
                    
                },

                new User
                {
                    Id = 2,
                    Fullname = "Helen Kebede",
                    Email = "helen@gmail.com",
                    Password = "helen123@"
                    
                }
                );
        }

    }

