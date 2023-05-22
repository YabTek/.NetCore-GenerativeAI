
using AutoMapper;
using Moq;
using Shouldly;
using TaskTracker.Application.Contracts.Persistence;
using TaskTracker.Application.Exceptions;
using TaskTracker.Application.Features.Tasks.Handlers.Commands;
using TaskTracker.Application.Features.Tasks.Requests.Commands;
using TaskTracker.Application.Profiles;
using TaskTracker.Tests.Mocks;
using Xunit;

namespace TaskTracker.Tests.Tasks.Commands;


  public class DeleteTaskCommandHandlerTests
    {
        private IMapper _mapper;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private DeleteTaskCommandHandler _handler;

        public DeleteTaskCommandHandlerTests()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

            _mapper = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            }).CreateMapper();

            _handler = new DeleteTaskCommandHandler(_mockUnitOfWork.Object, _mapper);
        }

        [Fact]
        public async Task DeleteTaskValid()
        {
            // Arrange
            var tasks = new Domain.task
            {
                Id = 1,
                Owner = 1,
                Title = "hey",
                Description = "This task is attending meeting",
                Start_date = DateTime.Today,
                End_date = DateTime.Now,
            };

            _mockUnitOfWork.Setup(x => x.TaskRepository.Get(tasks.Id)).ReturnsAsync(tasks);

            // Act
            await _handler.Handle(new DeleteTaskCommand() { Id = tasks.Id }, CancellationToken.None);

            // Assert
            _mockUnitOfWork.Verify(x => x.TaskRepository.Delete(tasks), Times.Once);
            _mockUnitOfWork.Verify(x => x.Save(), Times.Once);
        }

        [Fact]
        public async Task DeleteTaskInvalid()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.TaskRepository.Get(999)).ReturnsAsync((Domain.task)null);

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
                _handler.Handle(new DeleteTaskCommand() { Id = 999 }, CancellationToken.None));
        }
    }

