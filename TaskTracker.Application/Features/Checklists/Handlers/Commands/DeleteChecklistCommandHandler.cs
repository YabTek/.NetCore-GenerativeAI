using AutoMapper;
using MediatR;
using TaskTracker.Application.Contracts.Persistence;
using TaskTracker.Application.Exceptions;
using TaskTracker.Application.Features.Checklists.Requests.Commands;
using TaskTracker.Domain;

namespace TaskTracker.Application.Features.Checklists.Handlers.Commands;

    public class DeleteChecklistCommandHandler : IRequestHandler<DeleteChecklistCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteChecklistCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteChecklistCommand request, CancellationToken cancellationToken)
        {
            var checklist = await _unitOfWork.ChecklistRepository.Get(request.Id);

            if (checklist == null)
                throw new NotFoundException(nameof(Checklist), request.Id);

            await _unitOfWork.ChecklistRepository.Delete(checklist);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }

