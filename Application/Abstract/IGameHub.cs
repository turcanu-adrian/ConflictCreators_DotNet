using Domain.Enums;

namespace Application.Abstract
{
    public interface IGameHub
    {
        Task CreateNewGame(string nickname, GameType gameType, int maxGuestPlayers, string promptSetId);
        Task JoinGame(string name, string gameId);
        Task GetGame(string gameId);
        Task ContinueGame(string gameId);
        Task SendPlayerAnswer(string answer, string gameId);
    }
}
