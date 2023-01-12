using Application.Abstract;
using Domain.Enums;
using Domain.Games;
using MediatR;

namespace Application.Games.Base.Commands
{
    public class CheckAnswerCommand : IRequest<string>
    {
        public string GameId { get; set; } = null!;
    }

    public class CheckAnswerCommandHandler : IRequestHandler<CheckAnswerCommand, string>
    {
        private readonly IGameManager _gameManager;

        public CheckAnswerCommandHandler(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public async Task<string> Handle(CheckAnswerCommand command, CancellationToken token)
        {
            BaseGame game = _gameManager.GetGame(command.GameId);
            string rightAnswer = game.CurrentPrompt.CorrectAnswer;

            if (game.Type == GameType.WWTBAM)
                if (game.HostPlayer.Answer == rightAnswer && ((game as WWTBAMGame).CurrentTier + 1) % 5 == 0)
                {
                    game.HostPlayer.Points = (game as WWTBAMGame).Tiers[(game as WWTBAMGame).CurrentTier];
                }

            return await Task.FromResult(rightAnswer);
        }
    }
}
