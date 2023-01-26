using Application.Abstract;
using Domain.Games.Elements;
using MediatR;

namespace Application.PromptSets.Queries
{
    public class GetPromptsSetByIdQuery : IRequest<PromptSet>
    {
        public string Id { get; set; }
    }

    public class GetPromptsSetByIdQueryHandler : IRequestHandler<GetPromptsSetByIdQuery, PromptSet>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPromptsSetByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PromptSet> Handle(GetPromptsSetByIdQuery query, CancellationToken cancellationToken)
        {
            PromptSet promptSet = await _unitOfWork.PromptSetRepository.GetById(query.Id);

            return promptSet;
        }
    }
}
