using FluentValidation;

namespace TaskTracker.Application.DTOs.Checklists.Validators;

public class UpdateChecklistDtoValidator : AbstractValidator<UpdateChecklistDto>
{
    public UpdateChecklistDtoValidator(){

        Include(new IChecklistDtoValidator());
        RuleFor(x => x.Id)
            .NotNull().WithMessage("Id is required.")
            .GreaterThan(0).WithMessage("Id must be greater than 0.");


    }
}