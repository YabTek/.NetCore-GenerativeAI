using Moq;
using TaskTracker.Application.Contracts.Persistence;

namespace TaskTracker.Tests.Mocks;

    public static class MockUnitOfWork
    {
        public static Mock<IUnitOfWork> GetUnitOfWork()
        {
            var mockUow = new Mock<IUnitOfWork>();
            var mockUserRepo = MockUserRepository.GetUserRepository();
            var mockTaskRepo = MockTaskRepository.GetTaskRepository();
            var mockChecklistRepo = MockChecklistRepository.GetChecklistRepository();
            mockUow.Setup(r => r.TaskRepository).Returns(mockTaskRepo.Object);
            mockUow.Setup(r => r.UserRepository).Returns(mockUserRepo.Object);
            mockUow.Setup(r => r.ChecklistRepository).Returns(mockChecklistRepo.Object);

            mockUow.Setup(r => r.Save()).ReturnsAsync(1);
            return mockUow;
        }
    }


