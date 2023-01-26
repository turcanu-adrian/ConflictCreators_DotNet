using Application.Abstract;
using Domain.Games;
using MediatR;

namespace Application.Games.Base.Commands
{
    public class EndGameCommand : IRequest<BaseGame>
    {
        public string GameId { get; set; } = null!;
    }

    public class EndGameCommandHandler : IRequestHandler<EndGameCommand, BaseGame>
    {
        private readonly IGameManager _gameManager;

        public EndGameCommandHandler(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public Task<BaseGame> Handle(EndGameCommand command, CancellationToken cancellationToken)
        {
            BaseGame game = _gameManager.GetGame(command.GameId);
            game.EndTime = DateTime.Now;
            _gameManager.EndGame(game);
            return Task.FromResult(game);
        }
    }
}
