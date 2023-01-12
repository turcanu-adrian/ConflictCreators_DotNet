namespace Domain.Games.Elements
{
    public class Player
    {
        public Player(string nickname, string id)
        {
            Nickname = nickname;
            Points = 0;
            Avatar = "default";
            Id = id;
        }

        public Player(string nickname, string id, string avatar) : this(nickname, id)
        {
            Avatar = avatar;
        }

        public string Nickname { get; set; }
        public int Points { get; set; }
        public string Avatar { get; set; }
        public string Id { get; set; }
        public string Answer { get; set; }
    }
}
