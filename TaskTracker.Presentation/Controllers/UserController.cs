using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Application.DTOs.Users;
using TaskTracker.Application.Features.Users.Requests.Commands;
using TaskTracker.Application.Features.Users.Requests.Queries;
using TaskTracker.Application.Responses;

namespace TaskTracker.Presentation.Controllers;


    [Route("api/[Controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> Get()
        {
            var users = await _mediator.Send(new GetUserListRequest());
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> Get(int id)
        {
            var users = await _mediator.Send(new GetUserDetailRequest { Id = id });
            return Ok(users);
        }

        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateUserDto createUserDto)
        {
            var command = new CreateUserCommand { CreateUserDto = createUserDto };
            var repsonse = await _mediator.Send(command);
            return Ok(repsonse);
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UpdateUserDto userDto)
        {
            var command = new UpdateUserCommand { UpdateUserDto = userDto };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteUserCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }


