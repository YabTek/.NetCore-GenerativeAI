using TaskTracker.Application.Models;
using TaskTracker.Application.Responses;

namespace TaskTracker.Application.Contracts.Identity;

public interface IAuthService
{
    public Task<Result<RegistrationResponse>> Register(RegistrationRequest request);
    public Task<bool> DeleteUser(string Email);
    public Task<Result<AuthResponse>> Login(AuthRequest request);
}
