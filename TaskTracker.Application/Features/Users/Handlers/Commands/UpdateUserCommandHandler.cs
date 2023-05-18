using AutoMapper;
using MediatR;
using TaskTracker.Application.Contracts.Persistence;
using TaskTracker.Application.DTOs.Users.Validators;
using TaskTracker.Application.Exceptions;
using TaskTracker.Application.Features.Users.Requests.Commands;
using TaskTracker.Domain;

namespace TaskTracker.Application.Features.Users.Handlers.Commands;
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateUserDtoValidator();
            var validationResult = await validator.ValidateAsync(request.UpdateUserDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var user = await _unitOfWork.UserRepository.Get(request.UpdateUserDto.Id);

            if (user is null)
                throw new NotFoundException(nameof(User), request.UpdateUserDto.Id);

            _mapper.Map(request.UpdateUserDto, user);

            await _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }

