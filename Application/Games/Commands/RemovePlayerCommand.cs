using Application.Abstract;
using MediatR;

namespace Application.Game.Commands
{
    public class RemovePlayerCommand : IRequest<string>
    {
        public string playerId { get; set; } = null!;
        public string gameId { get; set; } = null!;
    }
    public class RemovePlayerCommandHandler : IRequestHandler<RemovePlayerCommand, string>
    {
        private readonly IGameManager _gameRepository;

        public RemovePlayerCommandHandler(IGameManager gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public Task<string> Handle(RemovePlayerCommand command, CancellationToken cancellationToken)
        {
            _gameRepository.RemovePlayerFromGame(command.playerId, command.gameId);
            return Task.FromResult(command.gameId);
        }

    }
}
