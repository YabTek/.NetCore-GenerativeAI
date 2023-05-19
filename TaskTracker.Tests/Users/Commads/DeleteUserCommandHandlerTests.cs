using AutoMapper;
using Moq;
using Shouldly;
using TaskTracker.Application.Contracts.Persistence;
using TaskTracker.Application.Exceptions;
using TaskTracker.Application.Features.Users.Handlers.Commands;
using TaskTracker.Application.Features.Users.Requests.Commands;
using TaskTracker.Application.Profiles;
using TaskTracker.Tests.Mocks;
using Xunit;

namespace TaskTracker.Tests.Users.Commads;

  public class DeleteUserCommandHandlerTests
    {
        private IMapper _mapper;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private DeleteUserCommandHandler _handler;

        public DeleteUserCommandHandlerTests()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

            _mapper = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            }).CreateMapper();

            _handler = new DeleteUserCommandHandler(_mockUnitOfWork.Object, _mapper);
        }

        [Fact]
        public async Task DeleteUserValid()
        {
            // Arrange
            var user = new Domain.User
            {
                Id = 1,
                Fullname = "John",
                Email = "johndoe@example.com",
                Password = "password"
            };

            _mockUnitOfWork.Setup(x => x.UserRepository.Get(user.Id)).ReturnsAsync(user);

            // Act
            await _handler.Handle(new DeleteUserCommand() { Id = user.Id }, CancellationToken.None);

            // Assert
            _mockUnitOfWork.Verify(x => x.UserRepository.Delete(user), Times.Once);
            _mockUnitOfWork.Verify(x => x.Save(), Times.Once);
        }

        [Fact]
        public async Task DeleteUserInvalid()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.UserRepository.Get(999)).ReturnsAsync((Domain.User)null);

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
                _handler.Handle(new DeleteUserCommand() { Id = 999 }, CancellationToken.None));
        }
    }

