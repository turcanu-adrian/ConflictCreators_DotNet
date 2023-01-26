using Application.Abstract;
using Application.Games.WWTBAM.Responses;
using Domain.Games;
using Domain.Enums;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System.Linq;

namespace Application.Games.Base.Queries
{
    public class GetGamesQuery : IRequest<WWTBAMResponse[]>
    {
        public string SearchValue { get; set; }
        public bool GetFull { get; set; }
        public bool GetEmpty { get; set; }
    }

    public class GetAllGamesQueryHandler : IRequestHandler<GetGamesQuery, WWTBAMResponse[]>
    {
        private readonly IGameManager _gameManager;

        public GetAllGamesQueryHandler(IGameManager gameManager)
        {
            _gameManager= gameManager;
        }

        public async Task<WWTBAMResponse[]> Handle(GetGamesQuery query, CancellationToken cancellationToken)
        {
            BaseGame[] games = _gameManager.GetAllGames().ToArray();

            if (!games.IsNullOrEmpty())
            {
                return games.Where(game => 
                    (game.CurrentPhase != GamePhase.gameover) &&
                    (game.HostPlayer.Nickname.ToLower().Contains(query.SearchValue.ToLower())) &&
                    (query.GetFull ? (game.GuestPlayers.Count <= game.MaxGuestPlayers) : (game.GuestPlayers.Count < game.MaxGuestPlayers )) &&
                    (query.GetEmpty ? (game.GuestPlayers.Count >= 0) : (game.GuestPlayers.Count > 0))).Select(game => new WWTBAMResponse(game as WWTBAMGame)).ToArray();
            }

            return null;
        }
    }
}
