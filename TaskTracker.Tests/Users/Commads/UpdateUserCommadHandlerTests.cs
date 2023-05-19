using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MediatR;
using Moq;
using TaskTracker.Application.Contracts.Persistence;
using TaskTracker.Application.DTOs.Users;
using TaskTracker.Application.Exceptions;
using TaskTracker.Application.Features.Users.Handlers.Commands;
using TaskTracker.Application.Features.Users.Requests.Commands;
using TaskTracker.Domain;
using Xunit;

namespace TaskTracker.Tests.Users.Commads;
    public class UpdateUserCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly UpdateUserCommandHandler _handler;

        public UpdateUserCommandHandlerTests()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UpdateUserDto, User>();
            });

            _mapper = mapperConfig.CreateMapper();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _handler = new UpdateUserCommandHandler(_mockUnitOfWork.Object, _mapper);
        }

        [Fact]
        public async Task UpdateUserCommandHandler_WhenUserExists_ShouldUpdateUser()
        {
            // Arrange
            var existingUserId = 1;
            var existingUser = new User
            {
                Id = existingUserId,
                Fullname = "John",
                Email = "johndoe@example.com",
                Password = "password"
            };
            var updateDto = new UpdateUserDto
            {
                Id = existingUserId,
                Fullname = "Jane",
                Email = "janedoe@example.com",
                Password = "newpassword"
            };
             var command = new UpdateUserCommand()
                        {
                        UpdateUserDto = updateDto
                        };
            _mockUnitOfWork.Setup(uow => uow.UserRepository.Get(existingUserId))
                .ReturnsAsync(existingUser);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockUnitOfWork.Verify(uow => uow.UserRepository.Update(It.IsAny<User>()), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.Save(), Times.Once);
            existingUser.Fullname.Should().Be(updateDto.Fullname);
            existingUser.Email.Should().Be(updateDto.Email);
            existingUser.Password.Should().Be(updateDto.Password);
        }

        [Fact]
        public async Task UpdateUserCommandHandler_WhenUserDoesNotExist_ShouldThrowNotFoundException()
        {
            // Arrange
            var nonExistingUserId = 1;
            var updateDto = new UpdateUserDto
            {
                Id = nonExistingUserId,
                Fullname = "Jane",
                Email = "janedoe@example.com",
                Password = "newpassword"
            };
          var command = new UpdateUserCommand()
                        {
                            UpdateUserDto = updateDto
                        };
            _mockUnitOfWork.Setup(uow => uow.UserRepository.Get(nonExistingUserId))
                .ReturnsAsync(() => null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }

