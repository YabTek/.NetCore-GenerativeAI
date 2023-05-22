using AutoMapper;
using FluentAssertions;
using Moq;
using Shouldly;
using TaskTracker.Application.Contracts.Persistence;
using TaskTracker.Domain;
using TaskTracker.Application.DTOs.Tasks;
using TaskTracker.Application.Features.Tasks.Handlers.Commands;
using TaskTracker.Application.Features.Tasks.Requests.Commands;
using TaskTracker.Application.Profiles;
using TaskTracker.Application.Responses;
using TaskTracker.Tests.Mocks;
using Xunit;

namespace TaskTracker.Tests.Tasks.Commands;

     public class CreateTaskCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly CreateTaskDto taskDto;
        private readonly CreateTaskDto _invalidTaskDto;

        private readonly CreateTaskCommandHandler _handler;

        public CreateTaskCommandHandlerTests()
        {
            _mockUow = MockUnitOfWork.GetUnitOfWork();
            
            var mapperConfig = new MapperConfiguration(c => 
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new CreateTaskCommandHandler(_mockUow.Object, _mapper);

            taskDto = new CreateTaskDto
            {
                    Owner = 1,
                    Title = "Attend meeting",
                    Description = "This task is attending meeting",
                    Start_date = DateTime.Today,
                    End_date = DateTime.Now,

            };

            _invalidTaskDto = new CreateTaskDto
            {
                    Owner = 1,
                    Title = "",
                    Description = "This task is attending meeting",
                    Start_date = DateTime.Today,
                    End_date = DateTime.Now,
                
            };
        }

        [Fact]
        public async Task Valid_Task_Added()
        {
            var result = await _handler.Handle(new CreateTaskCommand() { CreateTaskDto = taskDto }, CancellationToken.None);

            var tasks = await _mockUow.Object.TaskRepository.GetAll();

            result.ShouldBeOfType<BaseCommandResponse>();

            tasks.Count.ShouldBe(3);
        }

        [Fact]
        public async Task InValid_Task_Added()
        {

            var result = await _handler.Handle(new CreateTaskCommand() { CreateTaskDto = _invalidTaskDto}, CancellationToken.None);

            var tasks = await _mockUow.Object.UserRepository.GetAll();
            
            tasks.Count.ShouldBe(2);

            result.ShouldBeOfType<BaseCommandResponse>();
            
        }
    }


