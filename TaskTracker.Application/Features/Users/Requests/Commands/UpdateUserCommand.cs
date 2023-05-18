using MediatR;
using TaskTracker.Application.DTOs.Users;

namespace TaskTracker.Application.Features.Users.Requests.Commands;

public class UpdateUserCommand : IRequest<Unit>
{
    public UpdateUserDto UpdateUserDto { get; set; }
}
