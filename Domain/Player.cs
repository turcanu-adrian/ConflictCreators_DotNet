using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Player : BaseEntity
    {
        public Player(String nickname, String connectionId)
        {
            Nickname = nickname;
            Points = 0;
            Avatar = "default";
            ConnectionId = connectionId;
        }

        public Player(String nickname, String connectionId, String avatar) : this(nickname, connectionId)
        {
            Avatar = avatar;
        }

        public String Nickname { get; set; }
        public int Points { get; set; }
        public String Avatar { get; set; }
        public String ConnectionId { get; set; }

        public void SendInput()
        { 
        
        }
    }
}
