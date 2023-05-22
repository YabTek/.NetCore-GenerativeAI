using AutoMapper;
using FluentAssertions;
using MediatR;
using Moq;
using TaskTracker.Application.Contracts.Persistence;
using TaskTracker.Application.DTOs.Tasks;
using TaskTracker.Application.Exceptions;
using TaskTracker.Application.Features.Tasks.Handlers.Commands;
using TaskTracker.Application.Features.Tasks.Requests.Commands;
using TaskTracker.Domain;
using Xunit;
namespace TaskTracker.Tests.Tasks.Commands;


    public class UpdateTaskCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly UpdateTaskCommandHandler _handler;

        public UpdateTaskCommandHandlerTests()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UpdateTaskDto, task>();
            });

            _mapper = mapperConfig.CreateMapper();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _handler = new UpdateTaskCommandHandler(_mockUnitOfWork.Object, _mapper);
        }

        [Fact]
        public async Task UpdateTaskCommandHandler_WhenTaskExists_ShouldUpdateTask()
        {
            // Arrange
            var existingTaskId = 3;
            var existingTask = new task
            {
                Id = existingTaskId,
                Title = "meeting", 
                Description = "this is meeting" ,
                Owner = 1, 
                Start_date = DateTime.Today,
                End_date = DateTime.Now
            };
            var updateDto = new UpdateTaskDto
            {
               Id = existingTaskId,
               Title = "new meeting",
               Description = "this is new meeting" ,
               Owner = 1, 
               Start_date = DateTime.Today, 
               End_date = DateTime.Now
            };
             var command = new UpdateTaskCommand()
                        {
                        UpdateTaskDto = updateDto
                        };
            _mockUnitOfWork.Setup(uow => uow.TaskRepository.Get(existingTaskId))
                .ReturnsAsync(existingTask);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockUnitOfWork.Verify(uow => uow.TaskRepository.Update(It.IsAny<task>()), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.Save(), Times.Once);
            existingTask.Title.Should().Be(updateDto.Title);
            existingTask.Description.Should().Be(updateDto.Description);
            existingTask.Start_date.Should().Be(updateDto.Start_date);
            existingTask.End_date.Should().Be(updateDto.End_date);

        }

        [Fact]
        public async Task UpdateTaskCommandHandler_WhenTaskDoesNotExist_ShouldThrowNotFoundException()
        {
            // Arrange
            var nonExistingTaskId = 5;
            var updateDto = new UpdateTaskDto
            {
                Id = nonExistingTaskId,
                Title = "meeting", 
                Description = "this is meeting" ,
                Owner = 1, 
                Start_date = DateTime.Today,
                End_date = DateTime.Now
            };
          var command = new UpdateTaskCommand()
                        {
                            UpdateTaskDto = updateDto
                        };
            _mockUnitOfWork.Setup(uow => uow.TaskRepository.Get(nonExistingTaskId))
                .ReturnsAsync(() => null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }

