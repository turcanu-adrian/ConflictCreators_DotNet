using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Player : BaseEntity
    {
        public Player(String nickname, String Avatar)
        {
            Nickname = nickname;
            Points = 0;
            Avatar = "default";
        }
        public String Nickname { get; set; }
        public int Points { get; set; }
        public String Avatar { get; set; }

        public void SendInput()
        { 
        
        }
        public void JoinGame(String code)
        {

        }
    }
}
