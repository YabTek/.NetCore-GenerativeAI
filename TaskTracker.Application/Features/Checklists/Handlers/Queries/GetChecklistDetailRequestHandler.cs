using AutoMapper;
using MediatR;
using TaskTracker.Application.Contracts.Persistence;
using TaskTracker.Application.DTOs.Checklists;
using TaskTracker.Application.Features.Checklists.Requests.Queries;

namespace TaskTracker.Application.Features.Checklists.Handlers.Queries;
    public class GetChecklistDetailRequestHandler : IRequestHandler<GetChecklistDetailRequest, ChecklistDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetChecklistDetailRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ChecklistDto> Handle(GetChecklistDetailRequest request, CancellationToken cancellationToken)
        {
            var checklist = await _unitOfWork.ChecklistRepository.Get(request.Id);
            return _mapper.Map<ChecklistDto>(checklist);
        }
    
}
