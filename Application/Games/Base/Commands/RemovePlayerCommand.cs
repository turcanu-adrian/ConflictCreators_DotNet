using Application.Abstract;
using MediatR;

namespace Application.Games.Base.Commands
{
    public class RemovePlayerCommand : IRequest<string>
    {
        public string PlayerId { get; set; } = null!;
        public string GameId { get; set; } = null!;
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
            _gameRepository.RemovePlayerFromGame(command.PlayerId, command.GameId);
            return Task.FromResult(command.GameId);
        }

    }
}
