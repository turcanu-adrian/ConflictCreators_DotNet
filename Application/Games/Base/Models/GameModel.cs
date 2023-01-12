using Domain.Enums;
using Domain.Games.Elements;

namespace Application.Games.Base.Models
{
    public class GameModel
    {
        public GameType Type { get; set; }
        public PlayerModel HostPlayer { get; set; } = null!;
        public List<PlayerModel> GuestPlayers { get; set; } = null!;
        public GamePhase CurrentPhase { get; set; }
        public string Id { get; set; } = null!;
    }
}
