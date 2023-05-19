using AutoMapper;
using Moq;
using Shouldly;
using TaskTracker.Application.Contracts.Persistence;
using TaskTracker.Application.Exceptions;
using TaskTracker.Application.Features.Checklists.Handlers.Commands;
using TaskTracker.Application.Features.Checklists.Requests.Commands;
using TaskTracker.Application.Profiles;
using TaskTracker.Domain;
using TaskTracker.Tests.Mocks;
using Xunit;

namespace TaskTracker.Tests.Checklists.Commands;


  public class DeleteChecklistCommandHandlerTests
    {
        private IMapper _mapper;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private DeleteChecklistCommandHandler _handler;

        public DeleteChecklistCommandHandlerTests()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

            _mapper = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            }).CreateMapper();

            _handler = new DeleteChecklistCommandHandler(_mockUnitOfWork.Object, _mapper);
        }

        [Fact]
        public async Task DeleteChecklistValid()
        {
            // Arrange
            var checklist = new Domain.Checklist
            {
                    Id = 1,
                    Title = "create meeting agenda",
                    Description = "checklist for a meeting agenda",
                    associated_task = 3,
                    Status = ChecklistStatus.Completed
            };

            _mockUnitOfWork.Setup(x => x.ChecklistRepository.Get(checklist.Id)).ReturnsAsync(checklist);

            // Act
            await _handler.Handle(new DeleteChecklistCommand() { Id = checklist.Id }, CancellationToken.None);

            // Assert
            _mockUnitOfWork.Verify(x => x.ChecklistRepository.Delete(checklist), Times.Once);
            _mockUnitOfWork.Verify(x => x.Save(), Times.Once);
        }

        [Fact]
        public async Task DeleteChecklistInvalid()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.ChecklistRepository.Get(999)).ReturnsAsync((Domain.Checklist)null);

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
                _handler.Handle(new DeleteChecklistCommand() { Id = 999 }, CancellationToken.None));
        }
    }

