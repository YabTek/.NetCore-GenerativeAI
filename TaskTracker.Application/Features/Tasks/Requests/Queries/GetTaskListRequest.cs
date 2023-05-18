using MediatR;
using TaskTracker.Application.DTOs.Tasks;

namespace TaskTracker.Application.Features.Tasks.Requests.Queries;

public class GetTaskListRequest : IRequest<List<TaskDto>>
{
}
