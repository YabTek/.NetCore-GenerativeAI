using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskTracker.Application.Contracts.Persistence;
using TaskTracker.Persistence.Repository;

namespace TaskTracker.Persistence;
    public static class PersistenceServicesRegistration
    {
        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TaskTrackerDbContext>(opt =>
            opt.UseNpgsql(configuration.GetConnectionString("TaskTrackerConnectionString")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IChecklistRepository, ChecklistRepository>();
            
            return services;
        }
    }

