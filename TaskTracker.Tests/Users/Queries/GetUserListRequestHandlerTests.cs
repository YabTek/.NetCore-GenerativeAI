using AutoMapper;
using Moq;
using TaskTracker.Application.Contracts.Persistence;
using TaskTracker.Application.DTOs.Users;
using TaskTracker.Application.Features.Users.Handlers.Queries;
using TaskTracker.Application.Features.Users.Requests.Queries;
using TaskTracker.Domain;
using Xunit;

namespace TaskTracker.Tests.Users.Queries;

    public class GetUserListRequestHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;

        public GetUserListRequestHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task Handle_ReturnsListOfUserDtos_WhenUsersExist()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = 1, Fullname = "John Doe", Email = "johndoe@mail.com" },
                new User { Id = 2, Fullname = "Jane Doe", Email = "janedoe@mail.com" },
                new User { Id = 3, Fullname = "Bob Doe", Email = "bobsmith@mail.com" }
            };

            var userDtos = new List<UserDto>
            {
                new UserDto { Id = 1, Fullname = "John Doe", Email = "johndoe@mail.com" },
                new UserDto { Id = 2, Fullname = "Jane Doe", Email = "janedoe@mail.com" },
                new UserDto { Id = 3, Fullname = "Bob Doe", Email = "bobsmith@mail.com" }
            };

            _mockUnitOfWork.Setup(u => u.UserRepository.GetAll()).ReturnsAsync(users);
            _mockMapper.Setup(m => m.Map<List<UserDto>>(users)).Returns(userDtos);

            var handler = new GetUserListRequestHandler(_mockUnitOfWork.Object, _mockMapper.Object);

            // Act
            var result = await handler.Handle(new GetUserListRequest(), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<UserDto>>(result);
            Assert.Equal(userDtos.Count, result.Count);

            for (int i = 0; i < userDtos.Count; i++)
            {
                Assert.Equal(userDtos[i].Id, result[i].Id);
                Assert.Equal(userDtos[i].Fullname, result[i].Fullname);
                Assert.Equal(userDtos[i].Email, result[i].Email);
            }
        }

        [Fact]
        public async Task Handle_ReturnsEmptyList_WhenNoUsersExist()
        {
            // Arrange
            var users = new List<User>();
            var userDtos = new List<UserDto>();

            _mockUnitOfWork.Setup(u => u.UserRepository.GetAll()).ReturnsAsync(users);
            _mockMapper.Setup(m => m.Map<List<UserDto>>(users)).Returns(userDtos);

            var handler = new GetUserListRequestHandler(_mockUnitOfWork.Object, _mockMapper.Object);

            // Act
            var result = await handler.Handle(new GetUserListRequest(), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<UserDto>>(result);
            Assert.Empty(result);
        }
    }

