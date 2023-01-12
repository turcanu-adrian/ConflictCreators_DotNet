using Domain.Enums;
using Domain.Games;
using Domain.Games.Elements;

namespace Application.Abstract
{
    public interface IGameManager
    {
        void CreateGame(BaseGame game);
        PlayerType AddPlayerToGame(Player guestPlayer, string gameId);
        void RemovePlayerFromGame(string playerId, string gameId);
        BaseGame GetGame(string gameId);
        IEnumerable<BaseGame> GetGames();
        public string GetGameIdByPlayerId(string playerId);
        void EndGame(BaseGame game);
        void RemoveGame(BaseGame game);
    }
}