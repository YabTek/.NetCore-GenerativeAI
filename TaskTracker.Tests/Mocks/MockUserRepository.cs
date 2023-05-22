using Moq;
using TaskTracker.Application.Contracts.Persistence;
using TaskTracker.Domain;
using TaskTracker.Domain.Common;

namespace TaskTracker.Tests.Mocks;


public static class MockUserRepository
{
    public static Mock<IUserRepository> GetUserRepository()
    {
        var users = new List<User>
        {
            new ()
            {
                Id=1,
                Fullname = "Abebe kebede",
                Email = "Abe@gmail.com",
                Password = "hele123@"
                
            },
            
            new ()
            {
                Id = 2,
                Fullname = "Helen Kebede",
                Email = "helen@gmail.com",
                Password = "helen123@"
               
            }
        };
    var tasks = new List<task>
     {
        new task
        {
            Id = 3,
            Owner = 1,
            Title = "Attend meeting",
            Description = "This task is attending meeting",
            Start_date = DateTime.Today,
            End_date = DateTime.Now,
            Status = Status.Completed,    
        },  
        new task{
            Id = 4,
            Owner = 1,
            Title = "Attend ",
            Description = "This task is attending ",
            Start_date = DateTime.Today,
            End_date = DateTime.Now,
            Status = Status.Completed,      

        }};


        var mockRepo = new Mock<IUserRepository>();

        mockRepo.Setup(r => r.GetAll()).ReturnsAsync(users);
        
        mockRepo.Setup(r => r.Add(It.IsAny<User>())).ReturnsAsync((User user) =>
        {
            user.Id = users.Count() + 1;
            users.Add(user);
            return user; 
        });

        mockRepo.Setup(r => r.Update(It.IsAny<Domain.User>())).Callback((User user) =>
        {
            var newUsers = users.Where((r) => r.Id != user.Id);
            users = newUsers.ToList();
            users.Add(user);
        });
        
        mockRepo.Setup(r => r.Delete(It.IsAny<Domain.User>())).Callback((User user) =>
        {
            if (users.Exists(b => b.Id == user.Id))
                users.Remove(users.Find(b => b.Id == user.Id)!);
        });

        mockRepo.Setup(r => r.Exists(It.IsAny<int>())).ReturnsAsync((int id) =>
        {
            var user = users.FirstOrDefault((r) => r.Id == id);
            return user != null;
        });
        
        mockRepo.Setup(r => r.GetUserWithDetails(It.IsAny<int>(), It.IsAny<bool>()))
        .ReturnsAsync((int id, bool includeTask) =>
        {
            var user = users.FirstOrDefault(t => t.Id == id);

            if (includeTask && user != null)
            {
                user.Tasks = tasks
                    .Where(c => c.Owner == id)
                    .ToList();
            }

            return user;
        });

        return mockRepo;
    }
}