using Moq;
using TaskTracker.Application.Contracts.Persistence;
using TaskTracker.Domain;

namespace TaskTracker.Tests.Mocks;

public static class MockChecklistRepository
{
    public static Mock<IChecklistRepository> GetChecklistRepository()
    {
        var checklists = new List<Checklist>
        {
            new ()
            {
                    Id = 1,
                    Title = "create meeting agenda",
                    Description = "checklist for a meeting agenda",
                    associated_task = 3,
                    Status = ChecklistStatus.Completed
        
            },
            
            new ()
            {
                    Id = 2,
                    Title = "pack things",
                    Description = "checklist for packing things",
                    associated_task = 4,
                    Status = ChecklistStatus.Completed
            }
        };

        var mockRepo = new Mock<IChecklistRepository>();

         mockRepo.Setup(r => r.GetAll()).ReturnsAsync(checklists);
        
        mockRepo.Setup(r => r.Add(It.IsAny<Checklist>())).ReturnsAsync((Checklist checklist) =>
        {
            checklist.Id = checklists.Count() + 1;
            checklists.Add(checklist);
            return checklist; 
        });

        mockRepo.Setup(r => r.Update(It.IsAny<Domain.Checklist>())).Callback((Checklist checklist) =>
        {
            var newUsers = checklists.Where((r) => r.Id != checklist.Id);
            checklists = newUsers.ToList();
            checklists.Add(checklist);
        });
        
        mockRepo.Setup(r => r.Delete(It.IsAny<Domain.Checklist>())).Callback((Checklist checklist) =>
        {
            if (checklists.Exists(b => b.Id == checklist.Id))
                checklists.Remove(checklists.Find(b => b.Id == checklist.Id)!);
        });

        mockRepo.Setup(r => r.Exists(It.IsAny<int>())).ReturnsAsync((int id) =>
        {
            var checklist = checklists.FirstOrDefault((r) => r.Id == id);
            return checklist != null;
        });
        
        mockRepo.Setup(r => r.Get(It.IsAny<int>()))!.ReturnsAsync((int id) =>
        {
            return checklists.FirstOrDefault((r) => r.Id == id);
        });

        return mockRepo;
    }
}