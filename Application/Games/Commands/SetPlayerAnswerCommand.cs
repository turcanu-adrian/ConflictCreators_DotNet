using Application.Abstract;
using Domain.Games;
using Domain.Games.Elements;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Games.Commands
{
    public class SetPlayerAnswerCommand : IRequest<(string, string)>
    {
        public string playerId { get; set; } = null!;
        public string gameId { get; set; } = null!;
        public string answer { get; set; } = null!;
    }

    public class SetPlayerAnswerCommandHandler : IRequestHandler<SetPlayerAnswerCommand, (string, string)>
    {
        private readonly IGameManager _gameManager;
        private readonly IPromptRepository _promptRepository;

        public SetPlayerAnswerCommandHandler(IGameManager gameManager, IPromptRepository promptRepository)
        {
            _gameManager = gameManager;
            _promptRepository = promptRepository;
        }

        public async Task<(string, string)> Handle(SetPlayerAnswerCommand command, CancellationToken cancellationToken)
        {
            BaseGame game = _gameManager.GetGame(command.gameId);
            Player player = game.GetPlayer(command.playerId);
            player.Answer = command.answer;

            if (game.HostPlayer == player)
            {
                Task.Run(async () =>
                {
                    await Task.Delay(7000);
                    if (game.HostPlayer.Answer == game.CurrentPrompt.CorrectAnswer)
                    {
                        game.HostPlayer.Points += (game as WWTBAM).CurrentTier;
                        (game as WWTBAM).CurrentTier += 100;
                        game.CurrentPrompt = await _promptRepository.GetByUser("Gusky");
                    }
                    else
                    {
                        game.CurrentPhase = "gameover";
                        _gameManager.EndGame(game);
                    }
                });
                return ("host", game.CurrentPrompt.CorrectAnswer);
          
            }
            return ("guest", game.CurrentPrompt.CorrectAnswer);
        }
    }
}
