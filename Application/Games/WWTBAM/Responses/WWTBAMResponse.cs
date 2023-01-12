using Application.Games.Base.Responses;
using Domain.Enums;
using Domain.Games;
using Domain.Games.Elements;

namespace Application.Games.WWTBAM.Responses
{
    public class WWTBAMResponse : GameResponse
    {
        public WWTBAMResponse(WWTBAMGame game) : base(game)
        {
            Tiers = game.Tiers;
            AvailableCheats = game.AvailableCheats;
            ActiveCheats = game.ActiveCheats;
            CurrentTier = game.CurrentTier;
            if (ActiveCheats.Contains(Cheat.audienceAnswer))
                AudienceAnswers = GetAudienceAnswers(game.AudiencePlayers);
            
            if (ActiveCheats.Contains(Cheat.splitAnswers))
            {
                int firstAnswerToRemove = CurrentQuestionAnswers.FindIndex(s => s == game.CurrentPrompt.WrongAnswers[0]);
                int secondAnswerToRemove = CurrentQuestionAnswers.FindIndex(s => s == game.CurrentPrompt.WrongAnswers[1]);
                
                if (firstAnswerToRemove!=-1 && secondAnswerToRemove!=-1) 
                {
                    CurrentQuestionAnswers[firstAnswerToRemove] = "";
                    CurrentQuestionAnswers[secondAnswerToRemove] = "";
                }
            }
        }
        public int[] Tiers { get; set; }
        public int CurrentTier { get; set; }
        public List<Cheat> AvailableCheats { get; set; }
        public List<Cheat> ActiveCheats { get; set; }
        public Dictionary<string, float> AudienceAnswers { get; set; }

        private Dictionary<string, float> GetAudienceAnswers(List<Player> audiencePlayers)
        {
            Dictionary<string, float> audienceAnswers = new Dictionary<string, float>();
            
            audiencePlayers.ForEach(player =>
            {
                if (player.Answer != null)
                {
                    if (audienceAnswers.ContainsKey(player.Answer))
                        audienceAnswers[player.Answer]++;
                    else
                        audienceAnswers.Add(player.Answer, 1);
                }
            });

            int totalAnswers = (int)audienceAnswers.Sum(x => x.Value);

            foreach(KeyValuePair<string, float> entry in audienceAnswers)
            {
                audienceAnswers[entry.Key] = entry.Value / totalAnswers;
            }

            return audienceAnswers;
        }
    }
}
