using AutoMapper;
using Moq;
using TaskTracker.Application.Contracts.Persistence;
using TaskTracker.Application.DTOs.Users;
using TaskTracker.Application.Features.Users.Handlers.Queries;
using TaskTracker.Application.Features.Users.Requests.Queries;
using TaskTracker.Domain;
using Xunit;

namespace TaskTracker.Tests.Users.Queries;

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
        public async void Handle_WithValidQuery_ShouldReturnUserDto()
        {
            // Arrange
            var userId = 1;
            var user = new User { Id = userId, Fullname = "John Doe", Email = "johndoe@example.com" };
            var expectedUserDto = new UserDto { Id = userId, Fullname = "John Doe", Email = "johndoe@example.com" };
            var query = new GetUserDetailRequest { Id = userId };
            var cancellationToken = new CancellationToken();

            _mockUnitOfWork.Setup(uow => uow.UserRepository.Get(userId)).ReturnsAsync(user);
            _mockMapper.Setup(m => m.Map<UserDto>(user)).Returns(expectedUserDto);

            var handler = new GetUserDetailRequestHandler(_mockUnitOfWork.Object, _mockMapper.Object);

            // Act
            var result = await handler.Handle(query, cancellationToken);

            // Assert
            Assert.Equal(expectedUserDto, result);
        }
    }

