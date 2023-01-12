using Application.Abstract;
using Domain.Enums;
using Domain.Games;
using Domain.Games.Elements;
using MediatR;

namespace Application.Games.Base.Commands
{
    public class SetPlayerAnswerCommand : IRequest<PlayerType>
    {
        public string PlayerId { get; set; } = null!;
        public string GameId { get; set; } = null!;
        public string Answer { get; set; } = null!;
    }

    public class SetPlayerAnswerCommandHandler : IRequestHandler<SetPlayerAnswerCommand, PlayerType>
    {
        private readonly IGameManager _gameManager;

        public SetPlayerAnswerCommandHandler(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public async Task<PlayerType> Handle(SetPlayerAnswerCommand command, CancellationToken cancellationToken)
        {
            BaseGame game = _gameManager.GetGame(command.GameId);
            Player player = game.GetPlayer(command.PlayerId);
            player.Answer = command.Answer;

            if (game.HostPlayer == player)
                return PlayerType.host;
            return PlayerType.guest;
        }
    }
}
