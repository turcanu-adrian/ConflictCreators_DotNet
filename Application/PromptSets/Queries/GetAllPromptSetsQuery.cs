using Application.Abstract;
using Application.PromptSets.Responses;
using Domain.Games.Elements;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PromptSets.Queries
{
    public class GetAllPromptSetsQuery : IRequest<List<PromptSet>>
    {
    }

    public class GetPromptsBySetIdQueryHandler : IRequestHandler<GetAllPromptSetsQuery, List<PromptSet>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPromptsBySetIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<PromptSet>> Handle(GetAllPromptSetsQuery query, CancellationToken cancellationToken)
        {
            List<PromptSet> promptSets = await _unitOfWork.PromptSetRepository.GetAll();
            return promptSets;
        }
    }
}
