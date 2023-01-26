using Application.Games.Base.Models;
using Domain.Games;

namespace Application.Games.Base.Responses
{
    public class GameResponse : GameModel
    {
        public GameResponse(BaseGame game)
        {
            StartTime = game.StartTime;
            EndTime = game.EndTime;
            Id = game.Id;

            if (game.CurrentPrompt != null)
            {
                CurrentQuestion = game.CurrentPrompt.Question;
                CurrentQuestionAnswers = new List<string>();
                CurrentQuestionAnswers.AddRange(game.CurrentPrompt.WrongAnswers);
                CurrentQuestionAnswers.Add(game.CurrentPrompt.CorrectAnswer);
                ShuffleAnswers();
            }

            CurrentPhase = game.CurrentPhase;
            AudienceCount = game.AudiencePlayers.Count;

            HostPlayer = new PlayerModel
            {
                Avatar = game.HostPlayer.Avatar,
                Nickname = game.HostPlayer.Nickname,
                Points = game.HostPlayer.Points,
                Answer = game.HostPlayer.Answer,
                Id =  game.HostPlayer.Id,
                Badges= game.HostPlayer.Badges
            };

            GuestPlayers = game.GuestPlayers.Select(i => new PlayerModel
            {
                Avatar = i.Avatar,
                Nickname = i.Nickname,
                Points = i.Points,
                Answer = i.Answer,
                Id = i.Id,
                Badges = game.HostPlayer.Badges
            }).ToList();

            Type = game.Type;

            MaxGuestPlayers = game.MaxGuestPlayers;
        }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int AudienceCount { get; set; }
        public string? CurrentQuestion { get; set; }
        public List<string>? CurrentQuestionAnswers { get; set; }
        public int MaxGuestPlayers { get; set; }

        private void ShuffleAnswers()
        {
            if (CurrentQuestionAnswers != null)
            {
                CurrentQuestionAnswers = CurrentQuestionAnswers.OrderBy(x => CurrentQuestion.Length%x.Length).ToList();
            }
        }
    }
}
