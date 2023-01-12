using Domain.Games;
using MediatR;
using Application.Abstract;
using Domain.Games.Elements;

namespace Application.Games.Base.Commands
{
    public class CreateGameCommand : IRequest<string>
    {
        public string HostName { get; set; } = null!;
        public string HostConnectionId { get; set; } = null!;
        public string GameType { get; set; } = null!;
        public List<string> PromptsUsersFilter = null!;
    }

    public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, string>
    {
        private readonly IGameManager _gameRepository;
        public CreateGameCommandHandler(IGameManager gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public Task<string> Handle(CreateGameCommand command, CancellationToken cancellationToken)
        {
            var hostPlayer = new Player(command.HostName, command.HostConnectionId);
            
            /*if (command.GameType == "WWTBAM") 
            {
            }*/
            var game = new WWTBAMGame(hostPlayer, command.PromptsUsersFilter);

            _gameRepository.CreateGame(game);

            return Task.FromResult(game.Id);
        }
    }
}
