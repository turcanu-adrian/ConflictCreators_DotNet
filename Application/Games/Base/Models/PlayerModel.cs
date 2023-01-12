namespace Application.Games.Base.Models
{
    public class PlayerModel
    {
        public string Nickname { get; set; } = null!;
        public string? Avatar { get; set; } = null;
        public int Points { get; set; }
        public string? Answer { get; set; }
        public string Id { get; set; }
    }
}
