using Domain.Games;
using Domain;
using MediatR;
using Application.Abstract;

namespace Application.Game.Commands
{
    public class CreateGameCommand : IRequest<String>
    {
        public string HostName { get; set; } = null!;
        public string HostConnectionId { get; set; } = null!;
    }

    public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, String>
    {
        private readonly IGameManager _gameRepository;
        public CreateGameCommandHandler(IGameManager gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public Task<String> Handle(CreateGameCommand command, CancellationToken cancellationToken)
        {
            var hostPlayer = new Player(command.HostName, command.HostConnectionId);

            var game = new WWTBAM(hostPlayer);

            _gameRepository.CreateGame(game);

            return Task.FromResult(game.Id);
        }
    }
}
