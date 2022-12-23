using Application.Abstract;
using Domain;
using Domain.Games;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Game.Commands
{
    public class SetPlayerAnswerCommand : IRequest<String>
    {
        public string playerId { get; set; } = null!;
        public string gameId { get; set; } = null!;
        public string answer { get; set; } = null!;
    }

    public class SetPlayerAnswerCommandHandler : IRequestHandler<SetPlayerAnswerCommand, String>
    {
        private readonly IGameManager _gameManager;
        private readonly IPromptRepository _promptRepository;

        public SetPlayerAnswerCommandHandler(IGameManager gameManager, IPromptRepository promptRepository)
        {
            _gameManager = gameManager;
            _promptRepository = promptRepository;
        }

        public async Task<String> Handle(SetPlayerAnswerCommand command, CancellationToken cancellationToken)
        {
            BaseGame game = _gameManager.GetGame(command.gameId);
            Player player = game.GetPlayer(command.playerId);
            player.Answer = command.answer;

            if (game.HostPlayer == player)
            {
                if (game.HostPlayer.Answer == game.CurrentPrompt.CorrectAnswer)
                {
                    game.HostPlayer.Points += (game as WWTBAM).CurrentTier;
                    (game as WWTBAM).CurrentTier += 100;
                    game.CurrentPrompt = await _promptRepository.GetByUser("default");
                }
                else
                {
                    game.CurrentPhase = "gameover";
                    _gameManager.EndGame(game);
                }    
            }

            return game.Id;
        }
    }
}
