using Domain.Games;
using Application.Abstract;
using Domain.Games.Elements;
using Domain.Enums;

namespace Infrastructure
{
    public class GameManager : IGameManager
    {
        private readonly List<BaseGame> _games = new();
        private readonly Dictionary<string, string> _playerConnections = new();

        public void CreateGame(BaseGame game)
        {
            _games.Add(game);
            if (_playerConnections.ContainsKey(game.HostPlayer.Id))
                _playerConnections[game.HostPlayer.Id] = game.Id;
            else
                _playerConnections.Add(game.HostPlayer.Id, game.Id);
        }

        public void RemoveGame(BaseGame game)
        {
            _games.Remove(game);
            _playerConnections.Where(x => x.Value == game.Id).ToList().ForEach(it => _playerConnections.Remove(it.Key));
        }

        public PlayerType AddPlayerToGame(Player player, string gameId)
        {
            BaseGame game = _games.FirstOrDefault(o => o.Id.Equals(gameId));

            if (game != null)
            {
                _playerConnections.Add(player.Id, gameId);
                return game.AddPlayer(player);
            }

            return PlayerType.none;
        }

        public bool RemovePlayerFromGame(string playerId, string gameId)
        {
            BaseGame game = _games.FirstOrDefault(o => o.Id == gameId);

            if (game != null)
            {
                Player player = game.GuestPlayers.FirstOrDefault(o => o.Id == playerId) ?? game.AudiencePlayers.FirstOrDefault(o => o.Id == playerId) ?? game.HostPlayer;
                if (player != null)
                {
                    _playerConnections.Remove(playerId);
                    if (player == game.HostPlayer)
                        EndGame(game);
                    else
                        game.RemovePlayer(player);
                    return true;
                }
            }

            return false;
        }

        public BaseGame GetGame(string gameId)
        {
            BaseGame game = _games.FirstOrDefault(o => o.Id.Equals(gameId));

            return game;
        }

        public string GetGameIdByPlayerId(string playerId)
        {
            if (_playerConnections.ContainsKey(playerId))
                return _playerConnections[playerId];
            return null;
        }

        public IEnumerable<BaseGame> GetAllGames()
        {
            return _games;
        }

        public async void EndGame(BaseGame game)
        {
            game.CurrentPhase = GamePhase.gameover;
            await Task.Delay(10000);
            RemoveGame(game);
        }
    }
}