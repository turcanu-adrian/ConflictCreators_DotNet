using Application.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Game.Queries
{
    public class GetGameIdByPlayerIdQuery : IRequest<String>
        {
            public string PlayerId { get; set; } = null!;
        }
    
    public class GetGameIdByPlayerIdQueryHandler : IRequestHandler<GetGameIdByPlayerIdQuery, String>
        {
            private readonly IGameManager _gameRepository;

            public GetGameIdByPlayerIdQueryHandler(IGameManager gameRepository)
            {
                _gameRepository= gameRepository;
            }

            public Task<String> Handle(GetGameIdByPlayerIdQuery query, CancellationToken cancellationToken)
            {
                String gameId = _gameRepository.GetGameIdByPlayerId(query.PlayerId);
                return Task.FromResult(gameId);
            }
        }
}
