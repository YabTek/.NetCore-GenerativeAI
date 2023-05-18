using MediatR;
using TaskTracker.Application.DTOs.Checklists;

namespace TaskTracker.Application.Features.Checklists.Requests.Commands;

public class UpdateChecklistCommand : IRequest<Unit>
{
    public UpdateChecklistDto UpdateChecklistDto { get; set; }
}
