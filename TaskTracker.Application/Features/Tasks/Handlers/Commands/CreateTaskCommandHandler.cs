using AutoMapper;
using MediatR;
using TaskTracker.Application.Contracts.Persistence;
using TaskTracker.Application.DTOs.Tasks.Validators;
using TaskTracker.Application.Features.Tasks.Requests.Commands;
using TaskTracker.Application.Responses;
using TaskTracker.Domain;

namespace TaskTracker.Application.Features.Tasks.Handlers.Commands;
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateTaskCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateTaskDtoValidator();
            var validationResult = await validator.ValidateAsync(request.CreateTaskDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var new_task = _mapper.Map<task>(request.CreateTaskDto);

                new_task = await _unitOfWork.TaskRepository.Add(new_task);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = new_task.Id;
            }

            return response;
        }
    }


