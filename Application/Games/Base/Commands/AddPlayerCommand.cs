using Application.Abstract;
using Domain.Enums;
using Domain.Games.Elements;
using MediatR;

namespace Application.Games.Base.Commands
{
    public class AddPlayerCommand : IRequest<PlayerType>
    {
        public string GameId { get; set; } = null!;
        public string Nickname { get; set; } = null!;
        public string? Avatar { get; set; } = null;
        public string ConnectionId { get; set; } = null!;
    }

    public class AddPlayerCommandHandler : IRequestHandler<AddPlayerCommand, PlayerType>
    {
        private readonly IGameManager _gameManager;

        public AddPlayerCommandHandler(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        //Validator Method

        public Task<PlayerType> Handle(AddPlayerCommand command, CancellationToken cancellationToken)
        {
            Player player;
            if (command.Avatar != null)
                player = new Player(command.Nickname, command.ConnectionId, command.Avatar);
            else
                player = new Player(command.Nickname, command.ConnectionId);

            PlayerType result = _gameManager.AddPlayerToGame(player, command.GameId);

            return Task.FromResult(result);
        }
    }
}
