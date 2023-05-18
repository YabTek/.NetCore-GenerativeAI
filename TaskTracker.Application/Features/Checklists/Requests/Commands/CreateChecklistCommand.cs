using MediatR;
using TaskTracker.Application.DTOs.Checklists;
using TaskTracker.Application.Responses;

namespace TaskTracker.Application.Features.Checklists.Requests.Commands;

public class CreateChecklistCommand : IRequest<BaseCommandResponse>
{
    public CreateChecklistDto CreateChecklistDto { get; set; }
}
