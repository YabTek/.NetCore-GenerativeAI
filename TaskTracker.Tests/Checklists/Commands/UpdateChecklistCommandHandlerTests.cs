using AutoMapper;
using FluentAssertions;
using MediatR;
using Moq;
using TaskTracker.Application.Contracts.Persistence;
using TaskTracker.Application.DTOs.Checklists;
using TaskTracker.Application.Exceptions;
using TaskTracker.Application.Features.Checklists.Handlers.Commands;
using TaskTracker.Application.Features.Checklists.Requests.Commands;
using TaskTracker.Domain;
using Xunit;

namespace TaskTracker.Tests.Checklists.Commands;

    public class UpdateChecklistCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly UpdateChecklistCommandHandler _handler;

        public UpdateChecklistCommandHandlerTests()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UpdateChecklistDto, Checklist>();
            });

            _mapper = mapperConfig.CreateMapper();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _handler = new UpdateChecklistCommandHandler(_mockUnitOfWork.Object, _mapper);
        }

        [Fact]
        public async Task UpdateChecklistCommandHandler_WhenChecklistExists_ShouldUpdateChecklist()
        {
            // Arrange
            var existingChecklistId = 1;
            var existingChecklist = new Checklist
            {
                Id = existingChecklistId,
                Title = "create meeting agenda",
                Description = "checklist for a meeting agenda",
                associated_task = 3,
                Status = ChecklistStatus.Completed
            };
            var updateDto = new UpdateChecklistDto
            {
                Id = existingChecklistId,
                Title = "create new meeting agenda",
                Description = "checklist for a new meeting agenda",
                associated_task = 3,
                Status = ChecklistStatus.Completed
            };
             var command = new UpdateChecklistCommand()
                        {
                        UpdateChecklistDto = updateDto
                        };
            _mockUnitOfWork.Setup(uow => uow.ChecklistRepository.Get(existingChecklistId))
                .ReturnsAsync(existingChecklist);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockUnitOfWork.Verify(uow => uow.ChecklistRepository.Update(It.IsAny<Checklist>()), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.Save(), Times.Once);
            existingChecklist.Title.Should().Be(updateDto.Title);
            existingChecklist.Description.Should().Be(updateDto.Description);
            existingChecklist.associated_task.Should().Be(updateDto.associated_task);
            existingChecklist.Status.Should().Be(updateDto.Status);

        }

        [Fact]
        public async Task UpdateChecklistCommandHandler_WhenChecklistDoesNotExist_ShouldThrowNotFoundException()
        {
            // Arrange
            var nonExistingChecklistId = 5;
            var updateDto = new UpdateChecklistDto
            {
                Id = nonExistingChecklistId,
                Title = "create meeting agenda",
                Description = "checklist for a meeting agenda",
                associated_task = 3,
                Status = ChecklistStatus.Completed
            };
          var command = new UpdateChecklistCommand()
                        {
                            UpdateChecklistDto = updateDto
                        };
            _mockUnitOfWork.Setup(uow => uow.ChecklistRepository.Get(nonExistingChecklistId))
                .ReturnsAsync(() => null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }

