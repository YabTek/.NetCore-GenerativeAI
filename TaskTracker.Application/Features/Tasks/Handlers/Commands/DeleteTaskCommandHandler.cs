using AutoMapper;
using MediatR;
using TaskTracker.Application.Contracts.Persistence;
using TaskTracker.Application.Exceptions;
using TaskTracker.Application.Features.Tasks.Requests.Commands;
using TaskTracker.Domain;

namespace TaskTracker.Application.Features.Tasks.Handlers.Commands;

    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteTaskCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.TaskRepository.Get(request.Id);

            if (user == null)
                throw new NotFoundException(nameof(User), request.Id);

            await _unitOfWork.TaskRepository.Delete(user);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }

