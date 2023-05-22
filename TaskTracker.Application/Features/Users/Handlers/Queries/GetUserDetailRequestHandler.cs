using AutoMapper;
using MediatR;
using TaskTracker.Application.Contracts.Persistence;
using TaskTracker.Application.DTOs.Users;
using TaskTracker.Application.Features.Users.Requests.Queries;

namespace TaskTracker.Application.Features.Users.Handlers.Queries;

    public class GetUserDetailRequestHandler : IRequestHandler<GetUserDetailRequest, UserDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserDetailRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<UserDto> Handle(GetUserDetailRequest request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetUserWithDetails(request.Id, request.IncludeTask);
            return _mapper.Map<UserDto>(user);
        }
    
}
