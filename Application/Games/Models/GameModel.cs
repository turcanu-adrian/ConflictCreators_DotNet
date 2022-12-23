using Domain.Games.Elements;

namespace Application.Game.Models
{
    public class GameModel
    {
        public PlayerModel hostPlayer { get; set; } = null!;
        public string gamemode { get; set; } = null!;
        public List<PlayerModel> guestPlayers { get; set; } = null!;
        public string currentPhase { get; set; } = null!;
        public String Id { get; set; } = null!;
    }
}
