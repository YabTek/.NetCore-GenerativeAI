using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Application.DTOs.Tasks;
using TaskTracker.Application.Features.Tasks.Requests.Commands;
using TaskTracker.Application.Features.Tasks.Requests.Queries;
using TaskTracker.Application.Responses;

namespace TaskTracker.Presentation.Controllers;

    [Route("api/[Controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<TaskDto>>> Get()
        {
            var tasks = await _mediator.Send(new GetTaskListRequest());
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDto>> Get(int id)
        {
            var tasks = await _mediator.Send(new GetTaskDetailRequest { Id = id, IncludeChecklists = true, IncludeUser = true });
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateTaskDto createTaskDto)
        {
            var command = new CreateTaskCommand { CreateTaskDto = createTaskDto };
            var repsonse = await _mediator.Send(command);
            return Ok(repsonse);
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UpdateTaskDto taskDto)
        {
            var command = new UpdateTaskCommand { UpdateTaskDto = taskDto };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteTaskCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }


