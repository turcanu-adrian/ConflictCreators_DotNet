using Application.Abstract;
using Domain.Games.Elements;
using MediatR;

namespace Application.Prompts.Queries
{
    public class GetPromptsBySetIdQuery : IRequest<List<Prompt>>
    {
        public string PromptSetId { get; set; } 
    }

    public class GetPromptsBySetIdQueryHandler : IRequestHandler<GetPromptsBySetIdQuery, List<Prompt>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPromptsBySetIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Prompt>> Handle(GetPromptsBySetIdQuery query, CancellationToken cancellationToken)
        {
            List<Prompt> prompts = await _unitOfWork.PromptRepository.GetBySetId(query.PromptSetId);

            return prompts;
        }
    }
}
