using Domain.Enums;

namespace Application.Games.Base.Models
{
    public class PlayerModel
    {
        public string Nickname { get; set; } = null!;
        public Avatar Avatar { get; set; }
        public int Points { get; set; }
        public string? Answer { get; set; }
        public string Id { get; set; }
        public List<Badge> Badges { get; set; }
    }
}
