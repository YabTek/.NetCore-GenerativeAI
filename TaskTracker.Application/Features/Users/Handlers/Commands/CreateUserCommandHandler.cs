using AutoMapper;
using MediatR;
using TaskTracker.Application.Contracts.Persistence;
using TaskTracker.Application.DTOs.Users.Validators;
using TaskTracker.Application.Features.Users.Requests.Commands;
using TaskTracker.Application.Responses;
using TaskTracker.Domain;

namespace TaskTracker.Application.Features.Users.Handlers.Commands;

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateUserDtoValidator();
            var validationResult = await validator.ValidateAsync(request.CreateUserDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var user = _mapper.Map<User>(request.CreateUserDto);

                user = await _unitOfWork.UserRepository.Add(user);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = user.Id;
            }

            return response;
        }
    }

