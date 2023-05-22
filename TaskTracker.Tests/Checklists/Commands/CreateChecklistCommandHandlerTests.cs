using AutoMapper;
using Moq;
using Shouldly;
using TaskTracker.Application.Contracts.Persistence;
using TaskTracker.Application.DTOs.Checklists;
using TaskTracker.Application.Features.Checklists.Handlers;
using TaskTracker.Application.Features.Checklists.Requests.Commands;
using TaskTracker.Application.Profiles;
using TaskTracker.Application.Responses;
using TaskTracker.Domain;
using TaskTracker.Domain.Common;
using TaskTracker.Tests.Mocks;
using Xunit;

namespace TaskTracker.Tests.Checklists.Commands;


     public class CreateChecklistCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly CreateChecklistDto _checklistDto;
        private readonly CreateChecklistDto _invalidChecklistDto;

        private readonly CreateChecklistCommandHandler _handler;

        public CreateChecklistCommandHandlerTests()
        {
            _mockUow = MockUnitOfWork.GetUnitOfWork();
            
            var mapperConfig = new MapperConfiguration(c => 
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new CreateChecklistCommandHandler(_mockUow.Object, _mapper);

            _checklistDto = new CreateChecklistDto
            {
                Title = "create meeting agenda",
                Description = "checklist for a meeting agenda",
                associated_task = 3,
                Status = Status.Completed
            };

            _invalidChecklistDto = new CreateChecklistDto
            {
                Title = "",
                Description = "checklist for a meeting agenda",
                associated_task = 3,
                Status = Status.Completed
            };
        }

        [Fact]
        public async Task Valid_Checklist_Added()
        {
            var result = await _handler.Handle(new CreateChecklistCommand() { CreateChecklistDto = _checklistDto }, CancellationToken.None);

            var checklists = await _mockUow.Object.ChecklistRepository.GetAll();

            result.ShouldBeOfType<BaseCommandResponse>();

            checklists.Count.ShouldBe(3);
        }

        [Fact]
        public async Task InValid_Checklist_Added()
        {

            var result = await _handler.Handle(new CreateChecklistCommand() { CreateChecklistDto = _invalidChecklistDto}, CancellationToken.None);

            var checklists = await _mockUow.Object.ChecklistRepository.GetAll();
            
            checklists.Count.ShouldBe(2);

            result.ShouldBeOfType<BaseCommandResponse>();
            
        }
    }


