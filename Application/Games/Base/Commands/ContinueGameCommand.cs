using Application.Abstract;
using Domain.Games;
using MediatR;
using Domain.Enums;

namespace Application.Games.Base.Commands
{
    public class ContinueGameCommand : IRequest<Unit>
    {
        public string GameId { get; set; } = null!;
    }

    public class ContinueGameCommandHandler : IRequestHandler<ContinueGameCommand, Unit>
    {
        private readonly IGameManager _gameManager;
        private readonly IPromptRepository _promptRepository;

        public ContinueGameCommandHandler(IGameManager gameManager, IPromptRepository promptRepository)
        {
            _gameManager = gameManager;
            _promptRepository = promptRepository;
        }

        public async Task<Unit> Handle(ContinueGameCommand command, CancellationToken cancellationToken) 
        {
            BaseGame game = _gameManager.GetGame(command.GameId);
            game.CurrentPrompt = await _promptRepository.GetRandomBySet(game.PromptSetId);

            if (game.CurrentPhase == GamePhase.lobby)
            {
                game.CurrentPhase = GamePhase.prompt;
                game.StartTime = DateTime.Now;
            }
            else if (game.Type == GameType.WWTBAM)
            {
                (game as WWTBAMGame).CurrentTier++;
                (game as WWTBAMGame).ResetActiveCheats();
                game.ResetPlayerAnswers();
                if (((game as WWTBAMGame).CurrentTier) % 5 == 0)
                    (game as WWTBAMGame).ResetAvailableCheats();
            }

            return Unit.Value;
        }
    }
}
