using AutoMapper;
using Moq;
using Shouldly;
using TaskTracker.Application.Contracts.Persistence;
using TaskTracker.Application.DTOs.Users;
using TaskTracker.Application.Features.Users.Handlers.Commands;
using TaskTracker.Application.Features.Users.Requests.Commands;
using TaskTracker.Application.Profiles;
using TaskTracker.Application.Responses;
using TaskTracker.Tests.Mocks;
using Xunit;

namespace TaskTracker.Tests.Users.Commads;


     public class CreateUserCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly CreateUserDto _userDto;
        private readonly CreateUserDto _invalidUserDto;

        private readonly CreateUserCommandHandler _handler;

        public CreateUserCommandHandlerTests()
        {
            _mockUow = MockUnitOfWork.GetUnitOfWork();
            
            var mapperConfig = new MapperConfiguration(c => 
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new CreateUserCommandHandler(_mockUow.Object, _mapper);

            _userDto = new CreateUserDto
            {
                Fullname = "John",
                Email = "johndoe@example.com",
                Password = "password"
            };

            _invalidUserDto = new CreateUserDto
            {
                Fullname = "",
                Email = "johndoe@example.com",
                Password = "password"
            };
        }

        [Fact]
        public async Task Valid_User_Added()
        {
            var result = await _handler.Handle(new CreateUserCommand() { CreateUserDto = _userDto }, CancellationToken.None);

            var users = await _mockUow.Object.UserRepository.GetAll();

            result.ShouldBeOfType<BaseCommandResponse>();

            users.Count.ShouldBe(3);
        }

        [Fact]
        public async Task InValid_User_Added()
        {

            var result = await _handler.Handle(new CreateUserCommand() { CreateUserDto = _invalidUserDto}, CancellationToken.None);

            var users = await _mockUow.Object.UserRepository.GetAll();
            
            users.Count.ShouldBe(2);

            result.ShouldBeOfType<BaseCommandResponse>();
            
        }
    }


