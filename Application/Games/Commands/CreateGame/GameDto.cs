using Application.Games.Commands.AddPlayer;

namespace Application.Games.Commands.CreateGame
{
    public class GameDto
    {
        public PlayerDto hostPlayer { get; set; } = null!;
        public String gamemode { get; set; } = null!;
        public List<PlayerDto> guestPlayers { get; set; } 
        public String currentPhase { get; set; } = null!;
    }
}
