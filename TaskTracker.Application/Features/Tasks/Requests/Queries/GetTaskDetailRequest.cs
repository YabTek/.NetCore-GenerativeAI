using MediatR;
using TaskTracker.Application.DTOs.Tasks;
using TaskTracker.Domain;

namespace TaskTracker.Application.Features.Tasks.Requests.Queries;

public class GetTaskDetailRequest : IRequest<TaskDto>
{
    public int Id { get; set; }
    public bool IncludeChecklists { get; set; }
}
