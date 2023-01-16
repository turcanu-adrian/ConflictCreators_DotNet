using Domain.Enums;
using Domain.Games.Elements;

namespace Domain.Games
{
    public class WWTBAMGame : BaseGame
    {
        public WWTBAMGame(Player hostPlayer, List<string> promptSetsFilter) : base(hostPlayer, GameType.WWTBAM, promptSetsFilter)
        {
            Tiers = new int[] { 500, 1000, 2000, 3000, 5000, 7000, 10000, 20000, 30000, 50000, 100000, 250000, 500000, 750000, 1000000 };
            ResetAvailableCheats();
            ResetActiveCheats();
            CurrentTier = 0;
        }

        public List<Cheat> AvailableCheats { get; private set; }
        public List<Cheat> ActiveCheats { get; private set; }
        public int CurrentTier { get; set; }
        public int[] Tiers { get; set; }

        public void ResetAvailableCheats()
        {
            AvailableCheats = new List<Cheat>() { Cheat.guestAnswer, Cheat.audienceAnswer, Cheat.splitAnswers };
        }

        public void ResetActiveCheats()
        {
            ActiveCheats = new List<Cheat>() { };
        }

        public bool UseCheat(Cheat cheat)
        {
            if (AvailableCheats.Contains(cheat))
            {
                ActiveCheats.Add(cheat);
                AvailableCheats.Remove(cheat);
                return true;
            }
            return false;
        }
    }
}
