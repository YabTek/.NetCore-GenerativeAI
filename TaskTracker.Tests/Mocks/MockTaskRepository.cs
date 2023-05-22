using Moq;
using TaskTracker.Application.Contracts.Persistence;
using TaskTracker.Domain;
using TaskTracker.Domain.Common;

namespace TaskTracker.Tests.Mocks;


public static class MockTaskRepository
{
    public static Mock<ITaskRepository> GetTaskRepository()
    {
        var tasks = new List<task>
        {
            new ()
            {
                    Id = 3,
                    Owner = 1,
                    Title = "Attend meeting",
                    Description = "This task is attending meeting",
                    Start_date = DateTime.Today,
                    End_date = DateTime.Now,
                    Status = Status.Completed,                    
            },
            
            new ()
            {
                    Id = 4,
                    Owner = 1,
                    Title = "Attend class",
                    Description = "This task is attending class",
                    Start_date = DateTime.Today,
                    End_date = DateTime.Now,
                    Status = Status.Completed ,
            }
        };
         var checklists = new List<Checklist>
            {
                new Checklist
                {
                    Id = 1,
                    Title = "Checklist 1",
                    Description = "Checklist 1 description",
                    associated_task = 3,
                    Start_date = DateTime.Today,
                    End_date = DateTime.Now
                },
                new Checklist
                {
                    Id = 2,
                    Title = "Checklist 2",
                    Description = "Checklist 2 description",
                    associated_task = 3,
                    Start_date = DateTime.Today,
                    End_date = DateTime.Now
                }
            };
        var mockRepo = new Mock<ITaskRepository>();

        mockRepo.Setup(r => r.GetAll()).ReturnsAsync(tasks);
        
        mockRepo.Setup(r => r.Add(It.IsAny<task>())).ReturnsAsync((task task) =>
        {
            task.Id = tasks.Count() + 1;
            tasks.Add(task);
            return task; 
        });

        mockRepo.Setup(r => r.Update(It.IsAny<task>())).Callback((task task) =>
        {
            var newUsers = tasks.Where((r) => r.Id != task.Id);
            tasks = newUsers.ToList();
            tasks.Add(task);
        });
        
        mockRepo.Setup(r => r.Delete(It.IsAny<task>())).Callback((task task) =>
        {
            if (tasks.Exists(b => b.Id == task.Id))
                tasks.Remove(tasks.Find(b => b.Id == task.Id)!);
        });

        mockRepo.Setup(r => r.Exists(It.IsAny<int>())).ReturnsAsync((int id) =>
        {
            var task = tasks.FirstOrDefault((r) => r.Id == id);
            return task != null;
        });
        

        mockRepo.Setup(r => r.GetTaskWithDetails(It.IsAny<int>(), It.IsAny<bool>()))
        .ReturnsAsync((int id, bool includeChecklists) =>
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);

            if (includeChecklists && task != null)
            {
                task.Checklists = checklists
                    .Where(c => c.associated_task == id)
                    .ToList();
            }

            return task;
        });

    
        return mockRepo;
    }
}