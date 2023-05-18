using AutoMapper;
using MediatR;
using TaskTracker.Application.Contracts.Persistence;
using TaskTracker.Application.DTOs.Tasks;
using TaskTracker.Application.Features.Tasks.Requests.Queries;
using TaskTracker.Application.Profiles;

namespace TaskTracker.Application.Features.Tasks.Handlers.Queries;
    public class GetTaskDetailRequestHandler : IRequestHandler<GetTaskDetailRequest, TaskDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTaskDetailRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<TaskDto> Handle(GetTaskDetailRequest request, CancellationToken cancellationToken)
        {
            var taskk = await _unitOfWork.TaskRepository.Get(request.Id);
            return _mapper.Map<TaskDto>(taskk);
        }
    
}