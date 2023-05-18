using AutoMapper;
using MediatR;
using TaskTracker.Application.Contracts.Persistence;
using TaskTracker.Application.DTOs.Tasks;
using TaskTracker.Application.Features.Tasks.Requests.Queries;

namespace TaskTracker.Application.Features.Tasks.Handlers.Queries;

    public class GetTaskListRequestHandler : IRequestHandler<GetTaskListRequest, List<TaskDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTaskListRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<TaskDto>> Handle(GetTaskListRequest request, CancellationToken cancellationToken)
        {
            var tasks = await _unitOfWork.TaskRepository.GetAll();
            return _mapper.Map<List<TaskDto>>(tasks);
        }
    }

