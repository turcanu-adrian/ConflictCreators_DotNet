namespace Application.Game.Models
{
    public class PlayerModel
    {
        public string Nickname { get; set; } = null!;
        public string? Avatar { get; set; } = null;
        public int Points { get; set; }
        public String? Answer { get; set; }
    }
}
