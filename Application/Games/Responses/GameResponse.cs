using Application.Game.Models;
using Domain.Games;

namespace Application.Game.Responses
{
    public class GameResponse : GameModel
    {
        public int AudienceCount { get; set; }
        public String? CurrentQuestion { get; set; }
        public List<String>? CurrentQuestionAnswers { get; set; }

    }
}
