using Domain.Games;
using Domain;
using Application.Abstract;

namespace Application.GameManager
{
    public class GameManager : IGameManager
    {
        private readonly List<BaseGame> _games = new();
        private readonly Dictionary<String, String> _playerConnections = new();
   

        public void CreateGame(BaseGame game)
        {
            _games.Add(game);
            _playerConnections.Add(game.HostPlayer.Id, game.Id);
        }

        public void RemoveGame(BaseGame game)
        {
            _games.Remove(game);
            _playerConnections.Where(x => x.Value == game.Id).ToList().ForEach(it => _playerConnections.Remove(it.Key));
        }

        public void AddPlayerToGame(Player player, string gameId)
        {
            BaseGame game = _games.FirstOrDefault(o => o.Id.Equals(gameId));

            if (game == null)
                throw new InvalidOperationException($"Game with ID {gameId} not found");

            _playerConnections.Add(player.Id, gameId);
            game.AddPlayer(player);
        }

        public void RemovePlayerFromGame(string playerId, string gameId)
        {
            BaseGame game = _games.FirstOrDefault(o => o.Id == gameId);

            if (game == null)
                throw new InvalidOperationException($"Game with ID {gameId} not found");

            Player player = game.GuestPlayers.FirstOrDefault(o => o.Id == playerId) ?? game.AudiencePlayers.FirstOrDefault(o => o.Id == playerId);

            if (player == null)
                throw new InvalidOperationException($"Player with ID {playerId} not found");

            _playerConnections.Remove(playerId);
            game.RemovePlayer(player);
        }

        public BaseGame GetGame(string gameId)
        {
            BaseGame game = _games.FirstOrDefault(o => o.Id.Equals(gameId));
            if (game == null)
                throw new InvalidOperationException($"Game with ID {gameId} not found");

            return game;
        }

        public String GetGameIdByPlayerId(String playerId)
        {
            return _playerConnections[playerId];
        }

        public IEnumerable<BaseGame> GetGames()
        {
            return _games;
        }

        public async void EndGame(BaseGame game)
        {
            await Task.Delay(10000);
            RemoveGame(game);
            Console.WriteLine("Game removed");
        }
    }
}