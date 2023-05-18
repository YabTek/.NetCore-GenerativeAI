using MediatR;
using TaskTracker.Application.DTOs.Tasks;
using TaskTracker.Application.Responses;

namespace TaskTracker.Application.Features.Tasks.Requests.Commands;

public class CreateTaskCommand : IRequest<BaseCommandResponse>
{
    public CreateTaskDto CreateTaskDto { get; set; }
}
