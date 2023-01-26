using Application.Abstract;
using Domain.Enums;
using Domain.Games.Elements;
using MediatR;

namespace Application.Games.Base.Commands
{
    public class AddPlayerCommand : IRequest<PlayerType>
    {
        public string GameId { get; set; }
        public string Nickname { get; set; } 
        public Avatar Avatar { get; set; }
        public List<Badge> Badges { get; set; }
        public string ConnectionId { get; set; }
    }

    public class AddPlayerCommandHandler : IRequestHandler<AddPlayerCommand, PlayerType>
    {
        private readonly IGameManager _gameManager;

        public AddPlayerCommandHandler(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public Task<PlayerType> Handle(AddPlayerCommand command, CancellationToken cancellationToken)
        {
            Player player = new Player(command.Nickname, command.ConnectionId, command.Avatar, command.Badges);

            PlayerType result = _gameManager.AddPlayerToGame(player, command.GameId);

            return Task.FromResult(result);
        }
    }
}
