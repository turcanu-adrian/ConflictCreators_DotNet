using Domain.Games;
using MediatR;
using Application.Abstract;
using Domain.Games.Elements;
using Domain.Enums;

namespace Application.Games.Base.Commands
{
    public class CreateGameCommand : IRequest<string>
    {
        public string HostName { get; set; }
        public Avatar HostAvatar { get; set; }
        public List<Badge> HostBadges { get; set; }
        public string HostConnectionId { get; set; }
        public GameType GameType { get; set; }
        public int MaxGuestPlayers { get; set; }

        public string PromptSetId { get; set; }
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
            var hostPlayer = new Player(command.HostName, command.HostConnectionId, command.HostAvatar, command.HostBadges);
            
            var game = new WWTBAMGame(hostPlayer, command.MaxGuestPlayers, command.PromptSetId);

            _gameRepository.CreateGame(game);

            return Task.FromResult(game.Id);
        }
    }
}
