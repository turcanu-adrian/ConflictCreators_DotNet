using Application.Games.Commands.CreateGame;
using Application.Games.Queries.GetGames;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace SignalRTest.Hubs
{
    public class GameHub : Hub
    {
        private readonly IMediator _mediator;

        public GameHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task CreateNewGame(String HostName)
        {
            Console.WriteLine("Creating New Game");
            var gameId = await _mediator.Send(new CreateGameCommand
            {
                HostUsername = "Gusky",
                Game = new GameDto
                {
                    gamemode = "WWTBAM",
                    hostPlayerName = "Gusky"
                }
            });
            await Clients.All.SendAsync("ReceiveMessage", "New game created by " + HostName + " with the ID of " + gameId);
        }

        public async Task GetGame(String id)
        {
            var game = await _mediator.Send(new GetGameQuery
            {
                Id = id
            });
            await Clients.All.SendAsync("ReceiveMessage", "Game with id " + id + " being hosted by " + game.hostPlayerName + " with gamemode " + game.gamemode);
        }
    }
}
