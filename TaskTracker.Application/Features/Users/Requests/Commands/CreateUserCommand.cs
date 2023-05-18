using MediatR;
using TaskTracker.Application.DTOs.Users;
using TaskTracker.Application.Responses;

namespace TaskTracker.Application.Features.Users.Requests.Commands;

public class CreateUserCommand : IRequest<BaseCommandResponse>
{
    public CreateUserDto CreateUserDto { get; set; }

}
