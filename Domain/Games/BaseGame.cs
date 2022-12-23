
using Domain.Games.Elements;
using System.Numerics;

namespace Domain.Games
{
    public abstract class BaseGame : BaseEntity
    {
        public BaseGame(Player hostPlayer)
        {
            HostPlayer = hostPlayer;
            GuestPlayers = new();
            AudiencePlayers = new();
            CurrentPhase = "lobby";
            MaxGuestPlayers = 4;
        }

        public Player HostPlayer { get; }
        public List<Player> GuestPlayers { get; }
        public List<Player> AudiencePlayers { get; }
        public string CurrentPhase { get; set; }
        public int MaxGuestPlayers { get; }
        public Prompt? CurrentPrompt { get; set; } = null;

        public void AddPlayer(Player player)
        {
            if (GuestPlayers.Count < MaxGuestPlayers)
                GuestPlayers.Add(player);
            else
                AudiencePlayers.Add(player);
        }

        public void RemovePlayer(Player player)
        {
            if (GuestPlayers.Contains(player))
                GuestPlayers.Remove(player);
            else
                AudiencePlayers.Remove(player);
        }

        public Player GetPlayer(String playerId)
        {
            if (HostPlayer.Id == playerId) 
                return HostPlayer;
            return GuestPlayers.FirstOrDefault(o => o.Id == playerId) ?? AudiencePlayers.FirstOrDefault(o => o.Id == playerId);
        }
    }
}