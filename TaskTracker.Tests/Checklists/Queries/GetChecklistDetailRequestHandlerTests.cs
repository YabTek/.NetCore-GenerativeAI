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

    public class GetChecklistDetailRequestHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;

        public GetChecklistDetailRequestHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public async void Handle_WithValidQuery_ShouldReturnChecklistDto()
        {
            // Arrange
            var checklistId = 1;
            var checklist = new Checklist { Id = checklistId, Title = "create meeting agenda",
                    Description = "checklist for a meeting agenda", associated_task = 3,Status = Status.Completed };
            var expectedChecklistDto = new ChecklistDto { Id = checklistId, Title = "create meeting agenda",
                    Description = "checklist for a meeting agenda",associated_task = 3,Status = Status.Completed };
            var query = new GetChecklistDetailRequest { Id = checklistId };
            var cancellationToken = new CancellationToken();

            _mockUnitOfWork.Setup(uow => uow.ChecklistRepository.Get(checklistId)).ReturnsAsync(checklist);
            _mockMapper.Setup(m => m.Map<ChecklistDto>(checklist)).Returns(expectedChecklistDto);

            var handler = new GetChecklistDetailRequestHandler(_mockUnitOfWork.Object, _mockMapper.Object);

            // Act
            var result = await handler.Handle(query, cancellationToken);

            // Assert
            Assert.Equal(expectedChecklistDto, result);
        }
    }

