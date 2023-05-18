using TaskTracker.Application.DTOs.Users;
using FluentValidation;


namespace TaskTracker.Application.DTOs.Users.Validators;

public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>{
    public UpdateUserDtoValidator(){

        Include(new IUserDtoValidator());
        RuleFor(x => x.Id)
            .NotNull().WithMessage("Id is required.")
            .GreaterThan(0).WithMessage("Id must be greater than 0.");

    }
}

