using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstract
{
    public interface IGameClient
    {
        Task JoinedGameAs(string joinedAs);
        Task ReceiveGameState(string gameState);
        Task ReceiveRightAnswer(string rightAnswer);
    }
}
