using MediatR;
using TaskTracker.Application.DTOs.Users;

namespace TaskTracker.Application.Features.Users.Requests.Queries;

public class GetUserDetailRequest :  IRequest<UserDto>
{
    public int Id { get; set; }
    public bool IncludeTask { get; set; } 
}
