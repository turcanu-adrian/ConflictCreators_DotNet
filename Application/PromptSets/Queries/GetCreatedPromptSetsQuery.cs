using Application.Abstract;
using Domain;
using Domain.Games.Elements;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.PromptSets.Queries
{
    public class GetCreatedPromptSetsQuery : IRequest<List<PromptSet>>
    {
        public string  UserId { get; set; }
    }

    public class GetAllCreatedByUserIdQueryHandler : IRequestHandler<GetCreatedPromptSetsQuery, List<PromptSet>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllCreatedByUserIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<PromptSet>> Handle(GetCreatedPromptSetsQuery query, CancellationToken cancellationToken)
        {
            List<PromptSet> promptSets = await _unitOfWork.PromptSetRepository.GetAllCreatedBy(query.UserId);

            return promptSets;
        }
    }
}
