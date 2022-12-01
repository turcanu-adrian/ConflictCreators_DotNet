using Application.Games.Commands.CreateGame;
using MediatR;

namespace Application.Games.Queries.GetGames
{
    public class GetGameQuery : IRequest<GameDto>
    {
        public string Id { get; set; }
    }
}
