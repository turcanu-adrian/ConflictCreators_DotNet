using Domain.Enums;

namespace Domain.Games.Elements
{
    public class Player
    {
        public Player(string nickname, string id, Avatar avatar, List<Badge> badges)
        {
            Nickname = nickname;
            Points = 0;
            Avatar = avatar;
            Id = id;
            Badges = badges;
        }

        public string Nickname { get; set; }
        public int Points { get; set; }
        public Avatar Avatar { get; set; }
        public string Id { get; set; }
        public string Answer { get; set; }
        public List<Badge> Badges { get; set; }
    }
}
