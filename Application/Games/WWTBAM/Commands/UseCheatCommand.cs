using Application.Abstract;
using Domain.Enums;
using Domain.Games;
using MediatR;

namespace Application.Games.WWTBAM.Commands
{
    public class UseCheatCommand : IRequest<bool>
    {
        public string GameId { get; set; }
        public Cheat Cheat { get; set; }
    }
    
    public class UseCheatCommandHandler : IRequestHandler<UseCheatCommand, bool>
    {
        private readonly IGameManager _gameManager;

        public UseCheatCommandHandler(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public Task<bool> Handle(UseCheatCommand command, CancellationToken token)
        {
            WWTBAMGame game = (WWTBAMGame)_gameManager.GetGame(command.GameId);

            bool result = game.UseCheat(command.Cheat);

            return Task.FromResult(result);
        }
    }
}
