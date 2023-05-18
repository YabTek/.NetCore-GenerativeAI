using FluentValidation;

namespace TaskTracker.Application.DTOs.Checklists.Validators;

public class CreateChecklistDtoValidator : AbstractValidator<CreateChecklistDto>
{
    public CreateChecklistDtoValidator(){

        Include(new IChecklistDtoValidator());

    }
}