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
                gamemode = game.GetType().ToString(),
                hostPlayerName = game.HostPlayer.Nickname
            };

            Console.WriteLine(game.GetType().ToString());

            return Task.FromResult(gameDto);
        }
    }
}
