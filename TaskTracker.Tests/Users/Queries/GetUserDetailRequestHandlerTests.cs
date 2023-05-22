using AutoMapper;
using Moq;
using TaskTracker.Application.Contracts.Persistence;
using TaskTracker.Application.DTOs.Tasks;
using TaskTracker.Application.DTOs.Users;
using TaskTracker.Application.Features.Users.Handlers.Queries;
using TaskTracker.Application.Features.Users.Requests.Queries;
using TaskTracker.Domain;
using TaskTracker.Domain.Common;
using Xunit;

namespace TaskTracker.Tests.Users.Queries
{
    public class GetUserDetailRequestHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;

        public GetUserDetailRequestHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public async void Handle_WithValidQuery_ShouldReturnUserDtoWithTasks()
        {
            // Arrange
            var userId = 1;
            var user = new User { Id = userId, Fullname = "John Doe", Email = "johndoe@example.com" };

            // Simulate related tasks
            var task1 = new task { Id = 1, Title = "task 1", Description = "task 1 Description",Owner = userId,
                    Start_date = DateTime.Today,End_date = DateTime.Now,Status = Status.Completed};
            var task2 = new task { Id = 2, Title = "task 2", Description = "task 2 Description",Owner = userId,
                    Start_date = DateTime.Today,End_date = DateTime.Now,Status = Status.Completed };
            user.Tasks.Add(task1);
            user.Tasks.Add(task2);

            // Map related tasks to DTOs
            var expectedTaskDto1 = new TaskDto { Id = 1, Title = "task 1", Description = "task 1 Description",Owner = userId,
                    Start_date = DateTime.Today,End_date = DateTime.Now,Status = Status.Completed };
            var expectedTaskDto2 = new TaskDto { Id = 2, Title = "task 2", Description = "task 2 Description",Owner = userId,
                    Start_date = DateTime.Today,End_date = DateTime.Now,Status = Status.Completed };
           
            var expectedUserDto = new UserDto { Id = userId, Fullname = "John Doe",Email = "johndoe@example.com", 
            Tasks = new List<TaskDto> { expectedTaskDto1, expectedTaskDto2 }
              };

            var query = new GetUserDetailRequest { Id = userId , IncludeTask = true};
            var cancellationToken = new CancellationToken();

            _mockUnitOfWork.Setup(uow => uow.UserRepository.GetUserWithDetails(userId,true)).ReturnsAsync(user);
            _mockMapper.Setup(m => m.Map<UserDto>(user)).Returns(expectedUserDto);
            _mockMapper.Setup(m => m.Map<TaskDto>(task1)).Returns(expectedTaskDto1);
            _mockMapper.Setup(m => m.Map<TaskDto>(task2)).Returns(expectedTaskDto2);

            var handler = new GetUserDetailRequestHandler(_mockUnitOfWork.Object, _mockMapper.Object);

            // Act
            var result = await handler.Handle(query, cancellationToken);

            // Assert
            Assert.Equal(expectedUserDto, result);

            // Assert related tasks
            Assert.Contains(expectedTaskDto1, result.Tasks);
            Assert.Contains(expectedTaskDto2, result.Tasks);
        }
    }
}


