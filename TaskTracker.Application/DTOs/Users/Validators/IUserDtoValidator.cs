using FluentValidation;
using TaskTracker.Application.DTOs.Users;

namespace TaskTracker.Application.DTOs.Users.Validators;

public class IUserDtoValidator : AbstractValidator<IUserDto>
{
    public IUserDtoValidator()
    {
        RuleFor(x => x.Fullname)
            .NotEmpty().WithMessage("Fullname is required.")
            .MaximumLength(50).WithMessage("Fullname cannot exceed 50 characters.");
             
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email must be a valid email address.");
            
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");
    }
}