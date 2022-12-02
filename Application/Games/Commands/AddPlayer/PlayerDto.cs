namespace Application.Games.Commands.AddPlayer
{
    public class PlayerDto
    {
        public String nickname { get; set; } = null!;
        public String? avatar { get; set; } = null;
        public String connectionId { get; set; } = null!;
        public int points { get; set; }
    }
}
