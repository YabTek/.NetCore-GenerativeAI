using MediatR;
using TaskTracker.Application.DTOs.Users;

namespace TaskTracker.Application.Features.Users.Requests.Queries;

public class GetUserListRequest : IRequest<List<UserDto>>
{
}
