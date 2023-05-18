using AutoMapper;
using MediatR;
using TaskTracker.Application.Contracts.Persistence;
using TaskTracker.Application.DTOs.Checklists.Validators;
using TaskTracker.Application.Features.Checklists.Requests.Commands;
using TaskTracker.Application.Responses;
using TaskTracker.Domain;

namespace TaskTracker.Application.Features.Checklists.Handlers;

    public class CreateChecklistCommandHandler : IRequestHandler<CreateChecklistCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateChecklistCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateChecklistCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateChecklistDtoValidator();
            var validationResult = await validator.ValidateAsync(request.CreateChecklistDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var checklist = _mapper.Map<Checklist>(request.CreateChecklistDto);

                checklist = await _unitOfWork.ChecklistRepository.Add(checklist);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = checklist.Id;
            }

            return response;
        }
    }

