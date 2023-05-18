using MediatR;

namespace TaskTracker.Application.Features.Users.Requests.Commands;

public class DeleteUserCommand : IRequest
{
    public int Id { get; set; }
}
