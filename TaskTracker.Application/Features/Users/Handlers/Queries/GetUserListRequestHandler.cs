using AutoMapper;
using MediatR;
using TaskTracker.Application.Contracts.Persistence;
using TaskTracker.Application.DTOs.Users;
using TaskTracker.Application.Features.Users.Requests.Queries;


namespace BlogApp.Application.Features.Users.CQRS.Handlers
{
    public class GetUserListRequestHandler : IRequestHandler<GetUserListRequest, List<UserDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserListRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<UserDto>> Handle(GetUserListRequest request, CancellationToken cancellationToken)
        {
            var users = await _unitOfWork.UserRepository.GetAll();
            return _mapper.Map<List<UserDto>>(users);
        }
    }
}
