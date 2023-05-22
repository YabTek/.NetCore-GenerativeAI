using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Domain;
using TaskTracker.Domain.Common;

namespace TaskTracker.Persistence;

    public class TaskTrackerDbContext : DbContext
    {
        public TaskTrackerDbContext(DbContextOptions<TaskTrackerDbContext> options)
           : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<task>(entity => {
               entity.HasOne(u => u.User)
               .WithMany(t => t.Tasks)
               .HasForeignKey(x => x.Owner)
               .OnDelete(DeleteBehavior.Restrict)
               .HasConstraintName("FK_Task_User");

            });
            modelBuilder.Entity<Checklist>(entity => {

                entity.HasOne(u => u.Task)
               .WithMany(t => t.Checklists)
               .HasForeignKey(x => x.associated_task)
               .OnDelete(DeleteBehavior.Restrict)
               .HasConstraintName("FK_Task_Checklist");

            });
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskTrackerDbContext).Assembly);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<task> tasks { get; set; }
        public DbSet<Checklist> checklists { get; set; }
        

    }

