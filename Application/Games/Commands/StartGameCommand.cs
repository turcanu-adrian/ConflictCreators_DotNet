using Application.Abstract;
using Domain.Games;
using Domain.Games.Elements;
using MediatR;

namespace Application.Game.Commands
{
    public class StartGameCommand : IRequest<String>
    {
        public string gameId { get; set; } = null!;
    }

    public class StartGameCommandHandler : IRequestHandler<StartGameCommand, String>
    {
        private readonly IGameManager _gameManager;
        private readonly IPromptRepository _promptRepository;

        public StartGameCommandHandler(IGameManager gameManager, IPromptRepository promptRepository)
        {
            _gameManager = gameManager;
            _promptRepository = promptRepository;
        }

        public async Task<String> Handle(StartGameCommand command, CancellationToken cancellationToken)
        {
            BaseGame game = _gameManager.GetGame(command.gameId);

            game.CurrentPrompt = await _promptRepository.GetByUser("default");

            game.CurrentPhase = "prompt";

            Console.WriteLine("Started game " + command.gameId);
            return game.Id;
        }
    }
}
