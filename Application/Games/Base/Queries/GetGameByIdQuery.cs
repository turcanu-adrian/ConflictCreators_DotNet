using Application.Abstract;
using Application.Games.Base.Models;
using Application.Games.Base.Responses;
using Application.Games.WWTBAM.Responses;
using Domain.Enums;
using Domain.Games;
using MediatR;

namespace Application.Games.Base.Queries
{
    public class GetGameByIdQuery : IRequest<GameResponse>
    {
        public string Id { get; set; } = null!;
    }
    public class GetGameByIdQueryHandler : IRequestHandler<GetGameByIdQuery, GameResponse>
    {
        private readonly IGameManager _gameRepository;

        public GetGameByIdQueryHandler(IGameManager gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public Task<GameResponse> Handle(GetGameByIdQuery query, CancellationToken cancellationToken)
        {
            BaseGame game = _gameRepository.GetGame(query.Id);

            if (game == null)
                return null;

            if (game.Type == GameType.WWTBAM)
            {
                return Task.FromResult(new WWTBAMResponse(game as WWTBAMGame) as GameResponse);
            }

            return Task.FromResult(new GameResponse(game));
        }
    }
}
