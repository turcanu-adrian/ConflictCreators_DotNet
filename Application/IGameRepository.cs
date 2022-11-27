using Domain;

namespace Application
{
    public interface IGameRepository
    {
        void CreateGame(Game game);
        void AddGuestPlayerToGame(Player guestPlayer, String gameId);
        void RemoveGuestPlayerFromGame(Player guestPlayer, string gameId);
        Game GetGame(String gameId);
        IEnumerable<Game> GetGames();
    }
}