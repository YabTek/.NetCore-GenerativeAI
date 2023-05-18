using FluentValidation;
using TaskTracker.Application.DTOs.Tasks;

namespace TaskTracker.Application.DTOs.Tasks.Validators;

public class UpdateTaskDtoValidator : AbstractValidator<UpdateTaskDto>
{
    public  UpdateTaskDtoValidator()
    {
        Include(new ITaskDtoValidator());
        
        RuleFor(x => x.Id)
            .NotNull().WithMessage("Id is required.")
            .GreaterThan(0).WithMessage("Id must be greater than 0.");

    }
}
