using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Application.DTOs.Checklists;
using TaskTracker.Application.Features.Checklists.Requests.Commands;
using TaskTracker.Application.Features.Checklists.Requests.Queries;
using TaskTracker.Application.Responses;

namespace TaskTracker.Presentation.Controllers;

    [Route("api/[Controller]")]
    [ApiController]
    public class ChecklistController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ChecklistController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<ChecklistDto>>> Get()
        {
            var checklists = await _mediator.Send(new GetChecklistRequest());
            return Ok(checklists);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ChecklistDto>> Get(int id)
        {
            var checklists = await _mediator.Send(new GetChecklistDetailRequest { Id = id, IncludeTask = true });
            return Ok(checklists);
        }

        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateChecklistDto createChecklistDto)
        {
            var command = new CreateChecklistCommand { CreateChecklistDto = createChecklistDto };
            var repsonse = await _mediator.Send(command);
            return Ok(repsonse);
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UpdateChecklistDto checklistDto)
        {
            var command = new UpdateChecklistCommand { UpdateChecklistDto = checklistDto };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteChecklistCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }


