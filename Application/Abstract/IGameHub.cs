namespace Application.Abstract
{
    public interface IGameHub
    {
        Task CreateNewGame(String nickname);
        Task JoinGame(String name, String gameId);
        Task GetGame(String gameId);
        Task StartGame(String gameId);
        Task SendPlayerAnswer(String answer, String gameId);
    }
}
