namespace Application.Abstract
{
    public interface IGameHub
    {
        Task CreateNewGame(String nickname);
        Task JoinGame(String name, String gameId);
        Task GetGame(String gameId);
        Task ContinueGame(String gameId);
        Task SendPlayerAnswer(String answer, String gameId);
    }
}
