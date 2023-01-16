using Domain.Games;
using MediatR;
using Application.Abstract;
using Domain.Games.Elements;

namespace Application.Games.Base.Commands
{
    public class CreateGameCommand : IRequest<string>
    {
        public string HostName { get; set; } = null!;
        public string HostConnectionId { get; set; } = null!;
        public string GameType { get; set; } = null!;
        public List<string> promptSetsFilter = null!;
    }

    public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, string>
    {
        private readonly IGameManager _gameRepository;
        public CreateGameCommandHandler(IGameManager gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public Task<string> Handle(CreateGameCommand command, CancellationToken cancellationToken)
        {
            var hostPlayer = new Player(command.HostName, command.HostConnectionId);

            var game = new WWTBAMGame(hostPlayer, command.promptSetsFilter);

            _gameRepository.CreateGame(game);

            return Task.FromResult(game.Id);
        }
    }
}
