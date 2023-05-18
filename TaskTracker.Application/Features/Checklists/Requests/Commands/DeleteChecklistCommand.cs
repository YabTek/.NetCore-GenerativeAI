using MediatR;

namespace TaskTracker.Application.Features.Checklists.Requests.Commands;

public class DeleteChecklistCommand : IRequest
{
    public int Id { get; set; }
}
