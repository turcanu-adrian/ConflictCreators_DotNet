using Application.Abstract;
using MediatR;

namespace Application.Games.Base.Commands
{
    public class RemovePlayerCommand : IRequest<bool>
    {
        public string PlayerId { get; set; } = null!;
        public string GameId { get; set; } = null!;
    }
    public class RemovePlayerCommandHandler : IRequestHandler<RemovePlayerCommand, bool>
    {
        private readonly IGameManager _gameRepository;

        public RemovePlayerCommandHandler(IGameManager gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public Task<bool> Handle(RemovePlayerCommand command, CancellationToken cancellationToken)
        {
            bool result = _gameRepository.RemovePlayerFromGame(command.PlayerId, command.GameId);
            return Task.FromResult(result);
        }

    }
}
