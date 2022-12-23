using Application.Abstract;
using Application.Game.Models;
using Application.Game.Responses;
using Domain.Games;
using MediatR;

namespace Application.Game.Queries
{
    public class GetGameByIdQuery : IRequest<GameResponse>
    {
        public string Id { get; set; } = null!;
    }
    public class GetGameByIdQueryHandler : IRequestHandler<GetGameByIdQuery, GameResponse>
    {
        private readonly IGameManager _gameRepository;

        public GetGameByIdQueryHandler(IGameManager gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public Task<GameResponse> Handle(GetGameByIdQuery query, CancellationToken cancellationToken)
        {
            BaseGame game = _gameRepository.GetGame(query.Id);

            Console.WriteLine("GAME TYPE IS " + game.GetType().ToString());

            GameResponse gameResponse = CreateGameResponse(game);

            return Task.FromResult(gameResponse);
        }

        private GameResponse CreateGameResponse(BaseGame game)
        {
            GameResponse gameResponse = new GameResponse();
            gameResponse.Id = game.Id;
            gameResponse.gamemode = game.GetType().ToString().Split('.').Last();
            if (game.CurrentPrompt != null)
            {
                gameResponse.CurrentQuestion = game.CurrentPrompt.Question;
                gameResponse.CurrentQuestionAnswers = new List<String>();
                gameResponse.CurrentQuestionAnswers.AddRange(game.CurrentPrompt.WrongAnswers);
                gameResponse.CurrentQuestionAnswers.Add(game.CurrentPrompt.CorrectAnswer);
            }
            gameResponse.currentPhase = game.CurrentPhase;
            gameResponse.AudienceCount = game.AudiencePlayers.Count();
            gameResponse.hostPlayer = new PlayerModel
            {
                Avatar = game.HostPlayer.Avatar,
                Nickname = game.HostPlayer.Nickname,
                Points = game.HostPlayer.Points,
                Answer = game.HostPlayer.Answer
            };
            gameResponse.guestPlayers = game.GuestPlayers.Select(i => new PlayerModel
            {
                Avatar = i.Avatar,
                Nickname = i.Nickname,
                Points = i.Points,
                Answer = i.Answer
            }).ToList();

            return gameResponse;
        }
    }
}
