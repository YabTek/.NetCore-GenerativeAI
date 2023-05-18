using MediatR;
using TaskTracker.Application.DTOs.Checklists;
namespace TaskTracker.Application.Features.Checklists.Requests.Queries;

public class GetChecklistRequest : IRequest<List<ChecklistDto>>
{
}
