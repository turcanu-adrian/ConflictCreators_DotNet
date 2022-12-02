using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Games.Commands.JoinGame
{
    public class AddPlayerCommandHandler : IRequestHandler<AddPlayerCommand, String>
    {
        private readonly IGameRepository _gameRepository;

        public AddPlayerCommandHandler(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public Task<String> Handle(AddPlayerCommand command, CancellationToken cancellationToken)
        {
            Player player;
            if (command.player.avatar != null)
                player = new Player(command.player.nickname, command.player.connectionId, command.player.avatar);
            else
                player = new Player(command.player.nickname, command.player.connectionId);

            _gameRepository.AddGuestPlayerToGame(player, command.gameId);

            Console.WriteLine("Added player " + player.Nickname + " to game " + command.gameId);

            return Task.FromResult(command.gameId);
        }
    }
}
