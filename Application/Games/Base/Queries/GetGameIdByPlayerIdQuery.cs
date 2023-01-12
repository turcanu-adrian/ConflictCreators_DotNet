using Application.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Games.Base.Queries
{
    public class GetGameIdByPlayerIdQuery : IRequest<string>
    {
        public string PlayerId { get; set; } = null!;
    }

    public class GetGameIdByPlayerIdQueryHandler : IRequestHandler<GetGameIdByPlayerIdQuery, string>
    {
        private readonly IGameManager _gameRepository;

        public GetGameIdByPlayerIdQueryHandler(IGameManager gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public Task<string> Handle(GetGameIdByPlayerIdQuery query, CancellationToken cancellationToken)
        {
            string gameId = _gameRepository.GetGameIdByPlayerId(query.PlayerId);
            return Task.FromResult(gameId);
        }
    }
}
