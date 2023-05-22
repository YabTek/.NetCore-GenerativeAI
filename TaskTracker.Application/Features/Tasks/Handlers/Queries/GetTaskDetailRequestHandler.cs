using AutoMapper;
using MediatR;
using TaskTracker.Application.Contracts.Persistence;
using TaskTracker.Application.DTOs.Tasks;
using TaskTracker.Application.Features.Tasks.Requests.Queries;

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
            var task = await _unitOfWork.TaskRepository.GetTaskWithDetails(request.Id, request.IncludeChecklists);
            return _mapper.Map<TaskDto>(task);
        }
    
}
