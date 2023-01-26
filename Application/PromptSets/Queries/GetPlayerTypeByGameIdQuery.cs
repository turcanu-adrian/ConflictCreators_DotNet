using Application.Abstract;
using Domain.Enums;
using Domain.Games;
using Domain.Games.Elements;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PromptSets.Queries
{
    public class GetPlayerTypeByGameIdQuery : IRequest<PlayerType>
    {
        public string GameId { get; set; }
        public string PlayerId { get; set; }
    }

    public class GetPlayerTypeByGameIdQueryHandler : IRequestHandler<GetPlayerTypeByGameIdQuery, PlayerType>
    {
        private readonly IGameManager _gameManager;

        public GetPlayerTypeByGameIdQueryHandler(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public Task<PlayerType> Handle(GetPlayerTypeByGameIdQuery query, CancellationToken cancellationToken)
        {
            BaseGame game = _gameManager.GetGame(query.GameId);
            Player player = game.GetPlayer(query.PlayerId);

            if (game.HostPlayer == player)
                return Task.FromResult(PlayerType.host);

            if (game.GuestPlayers.Contains(player))
                return Task.FromResult(PlayerType.guest);

            if (game.AudiencePlayers.Contains(player))
                return Task.FromResult(PlayerType.audience);

            return Task.FromResult(PlayerType.none);
        }
    }
}
