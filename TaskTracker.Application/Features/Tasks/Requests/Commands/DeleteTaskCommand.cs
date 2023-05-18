using MediatR;

namespace TaskTracker.Application.Features.Tasks.Requests.Commands;

public class DeleteTaskCommand : IRequest
{
    public int Id { get; set; }
}
