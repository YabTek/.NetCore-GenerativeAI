using MediatR;
using TaskTracker.Application.DTOs.Tasks;

namespace TaskTracker.Application.Features.Tasks.Requests.Commands;

public class UpdateTaskCommand : IRequest<Unit>
{
    public UpdateTaskDto UpdateTaskDto { get; set; }
}
