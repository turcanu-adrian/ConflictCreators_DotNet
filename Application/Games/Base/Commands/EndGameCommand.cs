using Application.Abstract;
using Domain.Games;
using Domain.Games.Elements;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Games.Base.Commands
{
    public class EndGameCommand : IRequest<Unit>
    {
        public string GameId { get; set; } = null!;
    }

    public class EndGameCommandHandler : IRequestHandler<EndGameCommand, Unit>
    {
        private readonly IGameManager _gameManager;

        public EndGameCommandHandler(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public Task<Unit> Handle(EndGameCommand command, CancellationToken cancellationToken)
        {
            BaseGame game = _gameManager.GetGame(command.GameId);
            _gameManager.EndGame(game);
            return Task.FromResult(Unit.Value);
        }
    }
}
