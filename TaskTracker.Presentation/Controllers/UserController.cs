using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Application.DTOs.Users;
using TaskTracker.Application.Features.Users.Requests.Queries;

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
            var users = await _mediator.Send(new GetUserDetailRequest { Id = id, IncludeTask = true,});
            return Ok(users);
        }
    }


