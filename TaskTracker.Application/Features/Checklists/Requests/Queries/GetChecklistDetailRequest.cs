using MediatR;
using TaskTracker.Application.DTOs.Checklists;

namespace TaskTracker.Application.Features.Checklists.Requests.Queries;

public class GetChecklistDetailRequest : IRequest<ChecklistDto>
{
    public int Id { get; set; }
    public bool IncludeTask { get; set; } 
}
