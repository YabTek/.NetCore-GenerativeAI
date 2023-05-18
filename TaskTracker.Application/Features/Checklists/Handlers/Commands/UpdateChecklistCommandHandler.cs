using AutoMapper;
using MediatR;
using TaskTracker.Application.Contracts.Persistence;
using TaskTracker.Application.DTOs.Checklists.Validators;
using TaskTracker.Application.Exceptions;
using TaskTracker.Application.Features.Checklists.Requests.Commands;
using TaskTracker.Domain;

namespace TaskTracker.Application.Features.Checklists.Handlers.Commands;

    public class UpdateChecklistCommandHandler : IRequestHandler<UpdateChecklistCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateChecklistCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateChecklistCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateChecklistDtoValidator();
            var validationResult = await validator.ValidateAsync(request.UpdateChecklistDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var checklist = await _unitOfWork.ChecklistRepository.Get(request.UpdateChecklistDto.Id);

            if (checklist is null)
                throw new NotFoundException(nameof(Checklist), request.UpdateChecklistDto.Id);

            _mapper.Map(request.UpdateChecklistDto, checklist);

            await _unitOfWork.ChecklistRepository.Update(checklist);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }

