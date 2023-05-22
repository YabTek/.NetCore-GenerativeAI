using FluentValidation;

namespace TaskTracker.Application.DTOs.Checklists.Validators;

public class IChecklistDtoValidator : AbstractValidator<IChecklistDto>
{
   public  IChecklistDtoValidator(){

        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.associated_task).GreaterThan(0);
        RuleFor(x => x.Status).NotNull();
}
}