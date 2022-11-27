using System;

namespace Domain
{
    public abstract class Game : BaseEntity
    {
        public Game(Player hostPlayer)
        {
            HostPlayer = hostPlayer;
            GuestPlayers = new();
            AudiencePlayers = new();
            CurrentPhase = "lobby";
        }

        public Player HostPlayer { get; }
        public List<Player> GuestPlayers { get; }
        public List<Player> AudiencePlayers { get; }
        public string CurrentPhase { get; }

        public void AddGuestPlayer(Player guestPlayer)
        {
            GuestPlayers.Add(guestPlayer);
        }

        public void AddAudiencePlayer(Player audiencePlayer)
        {
            AudiencePlayers.Add(audiencePlayer);
        }

        public void RemoveGuestPlayer(Player guestPlayer)
        {
            GuestPlayers.Remove(guestPlayer);
        }

        public void RemoveAudiencePlayer(Player audiencePlayer)
        {
            AudiencePlayers.Remove(audiencePlayer);
        }
    }
}