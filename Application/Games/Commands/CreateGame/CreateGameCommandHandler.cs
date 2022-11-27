using Domain;
using Domain.Games;
using MediatR;

namespace Application.Games.Commands.CreateGame
{
    public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, String>
    {
        private readonly IGameRepository _gameRepository;
        public CreateGameCommandHandler(IGameRepository gameRepository) 
        {
            _gameRepository = gameRepository;
        }

        public Task<String> Handle(CreateGameCommand command, CancellationToken cancellationToken)
        {
            var hostPlayer = new Player(command.HostUsername);

            var game = new WWTBAM(hostPlayer);

            _gameRepository.CreateGame(game);

            return Task.FromResult(game.Id);
        }
    }
}
