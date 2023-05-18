using AutoMapper;
using MediatR;
using TaskTracker.Application.Contracts.Persistence;
using TaskTracker.Application.DTOs.Checklists;
using TaskTracker.Application.Features.Checklists.Requests.Queries;

namespace TaskTracker.Application.Features.Checklists.Handlers.Queries;

    public class GetChecklistRequestHandler : IRequestHandler<GetChecklistRequest, List<ChecklistDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetChecklistRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ChecklistDto>> Handle(GetChecklistRequest request, CancellationToken cancellationToken)
        {
            var checklists = await _unitOfWork.ChecklistRepository.GetAll();
            return _mapper.Map<List<ChecklistDto>>(checklists);
        }
    }

