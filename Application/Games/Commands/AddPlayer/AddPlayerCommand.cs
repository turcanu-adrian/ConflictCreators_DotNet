using Application.Games.Commands.AddPlayer;
using MediatR;

namespace Application.Games.Commands.JoinGame
{
    public class AddPlayerCommand : IRequest<String>
    {
        public String gameId { get; set; } = null!;
        public PlayerDto player { get; set; } = null!;
    }
}
