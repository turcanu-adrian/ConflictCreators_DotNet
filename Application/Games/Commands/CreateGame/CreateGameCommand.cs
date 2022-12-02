using MediatR;

namespace Application.Games.Commands.CreateGame
{
    public class CreateGameCommand : IRequest<String>
    {
        public String HostName { get; set; } = null!;
        public String HostConnectionId { get; set; } = null!;
        /*public GameDto Game { get; set; } = null!;*/
    }
}
