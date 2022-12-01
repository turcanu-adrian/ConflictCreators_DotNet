using Domain.Games;
using Application;
using Domain;

namespace Infrastructure
{
    public class InMemoryGameRepository : IGameRepository
    {
        private readonly List<Game> _games = new();

        public void CreateGame(Game game)
        {
            _games.Add(game);
        }

        public void AddGuestPlayerToGame(Player guestPlayer, String gameId)
        {
            var game = _games.FirstOrDefault(o => o.Id.Equals(gameId));
            if (game == null) 
                throw new InvalidOperationException($"Game with ID {gameId} not found");

            game.AddGuestPlayer(guestPlayer);
        }

        public void RemoveGuestPlayerFromGame(Player guestPlayer, string gameId) {
            var game = _games.FirstOrDefault(o => o.Id.Equals(gameId));
            if (game == null) 
                throw new InvalidOperationException($"Game with ID {gameId} not found");

            game.RemoveGuestPlayer(guestPlayer);
        }

        public Game GetGame(String gameId)
        {
            var game = _games.FirstOrDefault(o => o.Id.Equals(gameId));
            if (game == null) 
                throw new InvalidOperationException($"Game with ID {gameId} not found");

            return game;
        }

       public IEnumerable<Game> GetGames()
        {
            return _games;
        }
    }
}