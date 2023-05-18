using FluentValidation;
using TaskTracker.Application.DTOs.Tasks;

namespace TaskTracker.Application.DTOs.Tasks.Validators;

public class CreateTaskDtoValidator : AbstractValidator<CreateTaskDto>
{
    public CreateTaskDtoValidator(){

        Include(new ITaskDtoValidator());

    }
}
