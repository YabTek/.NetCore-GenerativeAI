using FluentValidation;
using TaskTracker.Application.DTOs.Users;

namespace TaskTracker.Application.DTOs.Users.Validators;

 public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            Include(new IUserDtoValidator());
        }
    }