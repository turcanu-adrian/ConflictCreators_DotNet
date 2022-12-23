
namespace Domain
{
    public class Player
    {
        public Player(String nickname, String id)
        {
            Nickname = nickname;
            Points = 0;
            Avatar = "default";
            Id = id;
        }

        public Player(String nickname, String id, String avatar) : this(nickname, id)
        {
            Avatar = avatar;
        }

        public String Nickname { get; set; }
        public int Points { get; set; }
        public String Avatar { get; set; }
        public String Id { get; set; }
        public String Answer { get; set; }
    }
}
