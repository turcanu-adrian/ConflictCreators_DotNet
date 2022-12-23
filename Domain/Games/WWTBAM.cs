using Domain.Games.Elements;

namespace Domain.Games
{
    public class WWTBAM : BaseGame
    {
        public WWTBAM(Player hostPlayer) : base(hostPlayer)
        {
            Cheats = new String[] { "OneGuy", "BackseatGaming", "JournalistMode" };
            CurrentTier = 100;
        }

        public String[] Cheats { get; }
        public int CurrentTier { get; set; }
        
    }
