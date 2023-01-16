
using Domain.Enums;
using Domain.Games.Elements;
using System.Numerics;

namespace Domain.Games
{
    public abstract class BaseGame : BaseEntity
    {
        public BaseGame(Player hostPlayer, GameType gameType, List<string> promptSetsFilter)
        {
            HostPlayer = hostPlayer;
            GuestPlayers = new();
            AudiencePlayers = new();
            CurrentPhase = GamePhase.lobby;
            MaxGuestPlayers = 4;
            Type = gameType;
            PromptSetsFilter = promptSetsFilter;
        }
        public List<string> PromptSetsFilter { get; set; }
        public GameType Type { get; set; }
        public Player HostPlayer { get; }
        public List<Player> GuestPlayers { get; }
        public List<Player> AudiencePlayers { get; }
        public GamePhase CurrentPhase { get; set; }
        public int MaxGuestPlayers { get; }
        public Prompt? CurrentPrompt { get; set; } = null;

        public PlayerType AddPlayer(Player player)
        {
            if (GuestPlayers.Count < MaxGuestPlayers)
            {
                GuestPlayers.Add(player);
                return PlayerType.guest;
            }
            AudiencePlayers.Add(player);
            return PlayerType.audience;
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

            Player player = GuestPlayers.FirstOrDefault(o => o.Id == playerId) ?? AudiencePlayers.FirstOrDefault(o => o.Id == playerId);

            return player;
        }
        
        public void ResetPlayerAnswers()
        {
            HostPlayer.Answer = null;
            GuestPlayers.ForEach(it => it.Answer = null);
            AudiencePlayers.ForEach(it => it.Answer = null);
        }
    }
}