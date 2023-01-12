using Application.Abstract;
using Domain.Games;
using MediatR;

namespace Application.Games.Base.Queries
{
    public class GetRightAnswerQuery : IRequest<string>
    {
        public string gameId { get; set; } = null!;
    }

    public class GetRightAnswerQueryHandler : IRequestHandler<GetRightAnswerQuery, string>
    {
        private readonly IGameManager _gameManager;
        private readonly IPromptRepository _promptRepository;

        public GetRightAnswerQueryHandler(IGameManager gameManager, IPromptRepository promptRepository)
        {
            _gameManager = gameManager;
            _promptRepository = promptRepository;
        }

        public async Task<string> Handle(GetRightAnswerQuery query, CancellationToken cancellationToken)
        {
            BaseGame game = _gameManager.GetGame(query.gameId);
            string rightAnswer = game.CurrentPrompt.CorrectAnswer;

            return rightAnswer;
        }
    }
}
