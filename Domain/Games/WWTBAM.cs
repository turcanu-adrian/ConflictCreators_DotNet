using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Games
{
    class WWTBAM : Game
    {
        public WWTBAM(Player hostPlayer) : base(hostPlayer)
        {
            Cheats = new String[] { "OneGuy", "BackseatGaming", "JournalistMode" };
            CurrentDifficulty = 0;
        }

        public String[] Cheats { get; }
        public int CurrentDifficulty { get; }
    }
}
