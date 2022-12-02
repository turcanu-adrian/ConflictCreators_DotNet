using Application.Games.Commands.AddPlayer;
using Application.Games.Commands.CreateGame;
using MediatR;

namespace Application.Games.Queries.GetGames
{
    internal class GetGameQueryHandler : IRequestHandler<GetGameQuery, GameDto>
    {
        private readonly IGameRepository _gameRepository;

        public GetGameQueryHandler(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public Task<GameDto> Handle(GetGameQuery query, CancellationToken cancellationToken)
        {
            var game = _gameRepository.GetGame(query.Id);

            var gameDto = new GameDto
            {
                gamemode = game.GetType().ToString().Split('.').Last(),
                hostPlayer = new PlayerDto
                {
                    avatar = game.HostPlayer.Avatar,
                    nickname= game.HostPlayer.Nickname,
                    points = game.HostPlayer.Points
                },
                guestPlayers = game.GuestPlayers.Select(i => new PlayerDto
                    {
                        avatar = i.Avatar,
                        nickname = i.Nickname,
                        points = i.Points
                    }).ToList()
            };

            return Task.FromResult(gameDto);
        }
    }
}
