using MediatR;

namespace Application.Games.Commands.CreateGame
{
    public class CreateGameCommand : IRequest<String>
    {
        public String HostUsername { get; set; } = null!;
        public GameDto Game { get; set; } = null!;

    }
}
