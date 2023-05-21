using Microsoft.AspNetCore.Mvc;
using MediatR;
using AutoMapper;
using TaskTracker.Application.Contracts.Identity;
using TaskTracker.Application.Models;
using TaskTracker.Application.Features.Users.Requests.Commands;
using TaskTracker.Application.DTOs.Users;
using TaskTracker.Application.Responses;

namespace TaskTracker.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : BaseController
{
    private readonly IAuthService _authService;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public AuthController(IAuthService authService, IMediator mediator, IMapper mapper)
    {
        _authService = authService;
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    [Route("Login")]
    public async Task<ActionResult<AuthResponse>> Login([FromBody] AuthRequest request)
    {
        var response = await _authService.Login(request);
        return HandleResult(response);
        
    }

    [HttpPost]
    [Route("Register")]
    public async Task<ActionResult<Result<RegistrationResponse>>> Register([FromBody] RegisterDto registerDto)
    {
        var response = await _authService.Register(_mapper.Map<RegistrationRequest>(registerDto));
        var command = new CreateUserCommand {CreateUserDto = _mapper.Map<CreateUserDto>(registerDto)};
        if (!response.Success || response.Value == null)
            return HandleResult(response);

        //command.CreateUserDto.Id = response.Value.UserId;
        try
        {
            var userResponse = await _mediator.Send(command);
            return HandleResult(response);
        }
        catch(Exception e)
        {
            await _authService.DeleteUser(registerDto.Email);
            response.Success = false;
            response.Errors.Add(e.Message);
            return HandleResult(response);
        }
        
    }
   

    [HttpDelete]
    [Route("Delete")]
    public async Task<ActionResult<bool>> Delete(string email)
    {
        var response = await _authService.DeleteUser(email);
        return Ok(response);  
    }
}