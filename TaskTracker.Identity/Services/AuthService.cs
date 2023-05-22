using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TaskTracker.Application.Contracts.Identity;
using TaskTracker.Application.Models;
using TaskTracker.Application.Models.Identity;
using TaskTracker.Application.Responses;
using TaskTracker.Identity.Models;

namespace TaskTracker.Identity.Services;

public class AuthService: IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ServerSettings _serverSettings;
    private readonly JwtSettings _jwtSettings;

    public AuthService(UserManager<AppUser> userManager,
                       SignInManager<AppUser> signInManager,
                       IOptions<JwtSettings> jwtSettings,
                       IOptions<ServerSettings> serverSettings
                       )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _serverSettings = serverSettings.Value;
        _jwtSettings = jwtSettings.Value;
    }
   
        
    public async Task<Result<AuthResponse>> Login(AuthRequest request)
    {
        Result<AuthResponse> result = new Result<AuthResponse>();
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            result.Success = false;
            result.Errors.Add($"User with given Email({request.Email}) doesn't exist");
            return result;
        }

        var res = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
        if(!res.Succeeded)
        {
            result.Success = false;
            result.Errors.Add($"Incorrect password");
            return result;
        }

        JwtSecurityToken token = await GenerateToken(user);

        var response = new AuthResponse()
        {
            UserId = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            Fullname = user.Fullname,
            Token = new JwtSecurityTokenHandler().WriteToken(token)
        };
    
        result.Value = response;
        return result;
    }
    private async Task<JwtSecurityToken> GenerateToken(AppUser user)
{
    var userClaims = await _userManager.GetClaimsAsync(user);

    var claims = new List<Claim>()
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim("uid", user.Id)
    }.Union(userClaims);

    var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
    var signingCredential = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
        issuer: _jwtSettings.Issuer,
        audience: _jwtSettings.Audience,
        claims: claims,
        expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
        signingCredentials: signingCredential
    );

    return token;
}


     public async Task<Result<RegistrationResponse>> Register(RegistrationRequest request)
{
    var result = new Result<RegistrationResponse>();
    var existingUser = await _userManager.FindByEmailAsync(request.Email);
    if (existingUser != null)
    {
        result.Success = false;
        result.Errors.Add($"User with given Email ({request.Email}) already exists");
        return result;
    }

    var user = new AppUser
    {
        UserName = request.UserName,
        Email = request.Email,
        EmailConfirmed = false
    };

    var createResult = await _userManager.CreateAsync(user, request.Password);

    if (!createResult.Succeeded)
    {
        result.Success = false;
        foreach (var error in createResult.Errors)
        {
            result.Errors.Add(error.Description);
        }
        return result;
    }

    try
    {
        var createdUser = await _userManager.FindByNameAsync(request.UserName);
        result.Success = true;
        result.Value = new RegistrationResponse
        {
            UserId = createdUser.Id,
            Email = createdUser.Email
        };

        return result;
    }
    catch (Exception ex)
    {
        result.Success = false;
        result.Errors.Add("An error occurred while saving the entity changes.");
        result.Errors.Add($"Inner exception message: {ex.InnerException?.Message}");
        return result;
    }
}


    // public async Task<Result<RegistrationResponse>> Register(RegistrationRequest request)
    // {
    //     var result = new Result<RegistrationResponse>();
    //     var existingUser = await _userManager.FindByEmailAsync(request.Email);
    //     if(existingUser != null)
    //     {
    //         result.Success = false;
    //         result.Errors.Add($"User with given Email({request.Email}) already exists");
    //         return result;
    //     }

    //     var user = new AppUser{
    //         UserName = request.UserName,
    //         Email = request.Email,
    //         EmailConfirmed = false
    //     };

    //     var createResult = await _userManager.CreateAsync(user, request.Password);

    //     if(!createResult.Succeeded)
    //     {
    //         result.Success = false;
    //         foreach(var Error in createResult.Errors)
    //         {
    //             result.Errors.Add(Error.Description);
    //         }
    //         return result;
    //     }
    //     var createdUser = await _userManager.FindByNameAsync(request.UserName);
    //     result.Success = true;
    //     result.Value = new RegistrationResponse
    //     {
    //         UserId = createdUser.Id,
    //         email = createdUser.Email
    //     };

    //     return result;
    // }

    public async Task<bool> DeleteUser(string Email)
    {
        var user = await _userManager.FindByEmailAsync(Email);
        if (user == null)
            return false;

        var result = await _userManager.DeleteAsync(user);
        return result.Succeeded;
    }
}
