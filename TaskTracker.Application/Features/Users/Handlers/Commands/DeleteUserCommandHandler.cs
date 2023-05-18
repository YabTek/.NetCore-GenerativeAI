using AutoMapper;
using MediatR;
using TaskTracker.Application.Contracts.Persistence;
using TaskTracker.Application.Exceptions;
using TaskTracker.Application.Features.Users.Requests.Commands;
using TaskTracker.Domain;

namespace TaskTracker.Application.Features.Users.Handlers.Commands;


    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.Get(request.Id);

            if (user == null)
                throw new NotFoundException(nameof(User), request.Id);

            await _unitOfWork.UserRepository.Delete(user);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }

