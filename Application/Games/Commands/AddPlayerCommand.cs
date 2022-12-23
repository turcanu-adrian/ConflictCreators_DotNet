using Application.Abstract;
using Domain;
using MediatR;

namespace Application.Game.Commands
{
    public class AddPlayerCommand : IRequest<string>
    {
        public string gameId { get; set; } = null!;
        public string nickname { get; set; } = null!;
        public string? avatar { get; set; } = null;
        public string connectionId { get; set; } = null!;
        public int points { get; set; }
    }

    public class AddPlayerCommandHandler : IRequestHandler<AddPlayerCommand, String>
    {
        private readonly IGameManager _gameManager;

        public AddPlayerCommandHandler(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        //Validator Method

        public Task<String> Handle(AddPlayerCommand command, CancellationToken cancellationToken)
        {
            Player player;
            if (command.avatar != null)
                player = new Player(command.nickname, command.connectionId, command.avatar);
            else
                player = new Player(command.nickname, command.connectionId);

            _gameManager.AddPlayerToGame(player, command.gameId);

            Console.WriteLine("Added player " + player.Nickname + " to game " + command.gameId);

            return Task.FromResult(command.gameId);
        }
    }
}
