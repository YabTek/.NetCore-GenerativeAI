using AutoMapper;
using Moq;
using Shouldly;
using TaskTracker.Application.Contracts.Persistence;
using TaskTracker.Application.DTOs.Checklists;
using TaskTracker.Application.DTOs.Tasks;
using TaskTracker.Application.Features.Tasks.Handlers.Queries;
using TaskTracker.Application.Features.Tasks.Requests.Queries;
using TaskTracker.Domain;
using Xunit;

namespace TaskTracker.Tests.Tasks.Queries
{
    public class GetTaskDetailRequestHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;

        public GetTaskDetailRequestHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public async void Handle_WithValidQuery_ShouldReturnTaskDtoWithChecklists()
        {
            // Arrange
            var taskId = 3;
            var task = new task { Id = taskId, Title = "meeting", Description = "this is meeting", Owner = 3, Start_date = DateTime.Today, End_date = DateTime.Now };

            // Simulate related checklists
            var checklist1 = new Checklist { Id = 1, Title = "Checklist 1", Description = "Checklist 1 description", associated_task = taskId, Start_date = DateTime.Today, End_date = DateTime.Now };
            var checklist2 = new Checklist { Id = 2, Title = "Checklist 2", Description = "Checklist 2 description", associated_task = taskId, Start_date = DateTime.Today, End_date = DateTime.Now };
            task.Checklists.Add(checklist1);
            task.Checklists.Add(checklist2);

            // Map related checklists to DTOs
            var expectedChecklistDto1 = new ChecklistDto { Id = 1, Title = "Checklist 1", Description = "Checklist 1 description", associated_task = taskId,
             Start_date = DateTime.Today, End_date = DateTime.Now };
            var expectedChecklistDto2 = new ChecklistDto { Id = 2, Title = "Checklist 2", Description = "Checklist 2 description", associated_task = taskId, 
            Start_date = DateTime.Today, End_date = DateTime.Now };

            var expectedTaskDto = new TaskDto
            {
                Id = taskId,
                Title = "meeting",
                Description = "this is meeting",
                Owner = 1,
                Start_date = DateTime.Today,
                End_date = DateTime.Now,
                Checklists = new List<ChecklistDto> { expectedChecklistDto1, expectedChecklistDto2 }
            };

            var query = new GetTaskDetailRequest { Id = taskId,IncludeChecklists = true};
            var cancellationToken = new CancellationToken();

            _mockUnitOfWork.Setup(uow => uow.TaskRepository.GetTaskWithDetails(taskId,true)).ReturnsAsync(task);
            _mockMapper.Setup(m => m.Map<TaskDto>(task)).Returns(expectedTaskDto);
            _mockMapper.Setup(m => m.Map<ChecklistDto>(checklist1)).Returns(expectedChecklistDto1);
            _mockMapper.Setup(m => m.Map<ChecklistDto>(checklist2)).Returns(expectedChecklistDto2);

            var handler = new GetTaskDetailRequestHandler(_mockUnitOfWork.Object, _mockMapper.Object);

            // Act
            var result = await handler.Handle(query, cancellationToken);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBe(expectedTaskDto);

            // Assert related checklists
            result.Checklists.ShouldContain(expectedChecklistDto1);
            result.Checklists.ShouldContain(expectedChecklistDto2);
        }
    }
}
