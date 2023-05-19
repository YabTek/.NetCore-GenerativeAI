using AutoMapper;
using Moq;
using TaskTracker.Application.Contracts.Persistence;
using TaskTracker.Application.DTOs.Tasks;
using TaskTracker.Application.Features.Tasks.Handlers.Queries;
using TaskTracker.Application.Features.Tasks.Requests.Queries;
using TaskTracker.Domain;
using Xunit;

namespace TaskTracker.Tests.Tasks.Queries;


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
        public async void Handle_WithValidQuery_ShouldReturnTaskDto()
        {
            // Arrange
            var taskId = 1;
            var tasks = new task { Id = taskId, Title = "meeting", Description = "this is meeting" ,Owner = 3, 
            Start_date = DateTime.Today, End_date = DateTime.Now};
            var expectedTaskDto = new TaskDto { Id = taskId, Title = "meeting", Description = "this is meeting" ,Owner = 3, 
            Start_date = DateTime.Today, End_date = DateTime.Now };
            
            var query = new GetTaskDetailRequest { Id = taskId };
            var cancellationToken = new CancellationToken();

            _mockUnitOfWork.Setup(uow => uow.TaskRepository.Get(taskId)).ReturnsAsync(tasks);
            _mockMapper.Setup(m => m.Map<TaskDto>(tasks)).Returns(expectedTaskDto);

            var handler = new GetTaskDetailRequestHandler(_mockUnitOfWork.Object, _mockMapper.Object);

            // Act
            var result = await handler.Handle(query, cancellationToken);

            // Assert
            Assert.Equal(expectedTaskDto, result);
        }
    }

