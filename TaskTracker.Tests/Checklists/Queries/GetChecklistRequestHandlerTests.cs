using AutoMapper;
using Moq;
using TaskTracker.Application.Contracts.Persistence;
using TaskTracker.Application.DTOs.Checklists;
using TaskTracker.Application.Features.Checklists.Handlers.Queries;
using TaskTracker.Application.Features.Checklists.Requests.Queries;
using TaskTracker.Domain;
using TaskTracker.Domain.Common;
using Xunit;

namespace TaskTracker.Tests.Checklists.Queries;

    public class GetChecklistRequestHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;

        public GetChecklistRequestHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task Handle_ReturnsListOfChecklistDtos_WhenChecklistExist()
        {
            // Arrange
            var checklists = new List<Checklist>
            {
                new Checklist { Id = 1, Title = "create meeting agenda",Description = "checklist for a meeting agenda",
                    associated_task = 3,Status = Status.Completed },
                new Checklist { Id = 2, Title = "create second meeting agenda",Description = "checklist for a meeting agenda",
                    associated_task = 4,Status = Status.Completed },
                new Checklist { Id = 3, Title = "create third meeting agenda",Description = "checklist for a meeting agenda",
                    associated_task = 6,Status = Status.Completed }
            };

            var checklistDtos = new List<ChecklistDto>
            {
                new ChecklistDto { Id = 1, Title = "create meeting agenda",Description = "checklist for a meeting agenda",
                    associated_task = 3,Status = Status.Completed },
                new ChecklistDto { Id = 2, Title = "create second meeting agenda",Description = "checklist for a meeting agenda",
                    associated_task = 4,Status = Status.Completed },
                new ChecklistDto { Id = 3,Title = "create third meeting agenda",Description = "checklist for a meeting agenda",
                    associated_task = 6,Status = Status.Completed  }
            };

            _mockUnitOfWork.Setup(u => u.ChecklistRepository.GetAll()).ReturnsAsync(checklists);
            _mockMapper.Setup(m => m.Map<List<ChecklistDto>>(checklists)).Returns(checklistDtos);

            var handler = new GetChecklistRequestHandler(_mockUnitOfWork.Object, _mockMapper.Object);

            // Act
            var result = await handler.Handle(new GetChecklistRequest(), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<ChecklistDto>>(result);
            Assert.Equal(checklistDtos.Count, result.Count);

            for (int i = 0; i < checklistDtos.Count; i++)
            {
                Assert.Equal(checklistDtos[i].Id, result[i].Id);
                Assert.Equal(checklistDtos[i].Title, result[i].Title);
                Assert.Equal(checklistDtos[i].Description, result[i].Description);
                Assert.Equal(checklistDtos[i].associated_task, result[i].associated_task);
                Assert.Equal(checklistDtos[i].Status, result[i].Status);

            }
        }

        [Fact]
        public async Task Handle_ReturnsEmptyList_WhenNoChecklistExists()
        {
            // Arrange
            var checklists = new List<Checklist>();
            var checklistDtos = new List<ChecklistDto>();

            _mockUnitOfWork.Setup(u => u.ChecklistRepository.GetAll()).ReturnsAsync(checklists);
            _mockMapper.Setup(m => m.Map<List<ChecklistDto>>(checklists)).Returns(checklistDtos);

            var handler = new GetChecklistRequestHandler(_mockUnitOfWork.Object, _mockMapper.Object);

            // Act
            var result = await handler.Handle(new GetChecklistRequest(), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<ChecklistDto>>(result);
            Assert.Empty(result);
        }
    }

