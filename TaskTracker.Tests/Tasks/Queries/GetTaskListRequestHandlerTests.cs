using AutoMapper;
using Moq;
using TaskTracker.Application.Contracts.Persistence;
using TaskTracker.Application.DTOs.Tasks;
using TaskTracker.Application.Features.Tasks.Handlers.Queries;
using TaskTracker.Application.Features.Tasks.Requests.Queries;
using TaskTracker.Domain;
using Xunit;

namespace TaskTracker.Tests.Tasks.Queries;

    public class GetTaskListRequestHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;

        public GetTaskListRequestHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task Handle_ReturnsListOfTaskDtos_WhenUsersExist()
        {
            // Arrange
            var tasks = new List<task>
            {
                new task { Id = 1, Title = "meeting", Description = "" ,Owner = 3, 
            Start_date = DateTime.Today, End_date = DateTime.Now },
                new task { Id = 2, Title = "meeting", Description = "" ,Owner = 3, 
            Start_date = DateTime.Today, End_date = DateTime.Now},
                new task { Id = 3, Title = "meeting", Description = "" ,Owner = 3, 
            Start_date = DateTime.Today, End_date = DateTime.Now }
            };

            var taskDtos = new List<TaskDto>
            {
                new TaskDto {  Id = 1, Title = "meeting", Description = "" ,Owner = 3, 
            Start_date = DateTime.Today, End_date = DateTime.Now  },
                new TaskDto {  Id = 1, Title = "meeting", Description = "" ,Owner = 3, 
            Start_date = DateTime.Today, End_date = DateTime.Now },
                new TaskDto { Id = 1, Title = "meeting", Description = "" ,Owner = 3, 
            Start_date = DateTime.Today, End_date = DateTime.Now  }
            };

            _mockUnitOfWork.Setup(u => u.TaskRepository.GetAll()).ReturnsAsync(tasks);
            _mockMapper.Setup(m => m.Map<List<TaskDto>>(tasks)).Returns(taskDtos);

            var handler = new GetTaskListRequestHandler(_mockUnitOfWork.Object, _mockMapper.Object);

            // Act
            var result = await handler.Handle(new GetTaskListRequest(), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<TaskDto>>(result);
            Assert.Equal(taskDtos.Count, result.Count);

            for (int i = 0; i < taskDtos.Count; i++)
            {
                Assert.Equal(taskDtos[i].Id, result[i].Id);
                Assert.Equal(taskDtos[i].Owner, result[i].Owner);
                Assert.Equal(taskDtos[i].Start_date, result[i].Start_date);
                Assert.Equal(taskDtos[i].End_date, result[i].End_date);
                Assert.Equal(taskDtos[i].Title, result[i].Title);
                Assert.Equal(taskDtos[i].Description, result[i].Description);
            }
        }

        [Fact]
        public async Task Handle_ReturnsEmptyList_WhenNoTaskExist()
        {
            // Arrange
            var tasks = new List<task>();
            var taskDtos = new List<TaskDto>();

            _mockUnitOfWork.Setup(u => u.TaskRepository.GetAll()).ReturnsAsync(tasks);
            _mockMapper.Setup(m => m.Map<List<TaskDto>>(tasks)).Returns(taskDtos);

            var handler = new GetTaskListRequestHandler(_mockUnitOfWork.Object, _mockMapper.Object);

            // Act
            var result = await handler.Handle(new GetTaskListRequest(), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<TaskDto>>(result);
            Assert.Empty(result);
        }
    }

