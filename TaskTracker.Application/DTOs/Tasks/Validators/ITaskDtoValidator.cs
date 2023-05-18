using FluentValidation;
using TaskTracker.Application.DTOs.Tasks;

namespace TaskTracker.Application.DTOs.Tasks.Validators;

public class ITaskDtoValidator : AbstractValidator<ITaskDto>
{
    public ITaskDtoValidator(){
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title cannot be empty.");
            
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description cannot be empty.");
            
            RuleFor(x => x.End_date)
                .GreaterThan(x => x.Start_date).WithMessage("End date must be after start date.");
    }
}
