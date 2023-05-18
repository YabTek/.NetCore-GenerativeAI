using AutoMapper;
using MediatR;
using TaskTracker.Application.Contracts.Persistence;
using TaskTracker.Application.DTOs.Tasks.Validators;
using TaskTracker.Application.Exceptions;
using TaskTracker.Application.Features.Tasks.Requests.Commands;
using TaskTracker.Domain;

namespace TaskTracker.Application.Features.Tasks.Handlers.Commands;

    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateTaskCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateTaskDtoValidator();
            var validationResult = await validator.ValidateAsync(request.UpdateTaskDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var taskk = await _unitOfWork.TaskRepository.Get(request.UpdateTaskDto.Id);

            if (taskk is null)
                throw new NotFoundException(nameof(task), request.UpdateTaskDto.Id);

            _mapper.Map(request.UpdateTaskDto, taskk);

            await _unitOfWork.TaskRepository.Update(taskk);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }


