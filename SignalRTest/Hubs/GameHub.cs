using Application.Games.Commands.CreateGame;
using Application.Games.Commands.JoinGame;
using Application.Games.Commands.AddPlayer;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Application.Games.Queries.GetGames;

namespace SignalRTest.Hubs
{
    public class GameHub : Hub
    {
        private readonly IMediator _mediator;

        public GameHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task OnConnectedAsync()
        {
            Console.WriteLine(Context.ConnectionId + " has connected");
        }

        public async Task CreateNewGame(String HostName)
        {
            Console.WriteLine("Creating New Game");

            var gameId = await _mediator.Send(new CreateGameCommand
            {
                HostName = "Gusky",
                HostConnectionId = Context.ConnectionId/*,
                Game = new GameDto
                {
                    gamemode = "WWTBAM",
                    hostPlayerName = "Gusky"
                }*/
            });

            await Groups.AddToGroupAsync(Context.ConnectionId, gameId);
            await Clients.Caller.SendAsync("ReceiveMessage", "New game created successfully with ID " + gameId);
        }

        public async Task JoinGame(String id)
        {
            var _gameId = await _mediator.Send(new AddPlayerCommand
            {
                gameId = id,
                player = new PlayerDto 
                {
                    nickname = "GuestPlayer651",
                    connectionId = Context.ConnectionId
                }
            });
            await Clients.Group(id).SendAsync("ReceiveMessage", "Player GuestPlayer651 has joined the game " + id);
            await Groups.AddToGroupAsync(Context.ConnectionId, _gameId);
        }

        public async Task GetGame(String id)
        {
            var game = await _mediator.Send(new GetGameQuery
            {
                Id = id
            });

            await Clients.Caller.SendAsync("ReceiveMessage", "Requested game with id " + id + " || Host is " + game.hostPlayer.nickname + "|| Guest players are " + String.Join("||", game.guestPlayers.Select(it => "Name=" + it.nickname + " Points=" + it.points + " Avatar=" + it.avatar).ToList()));
        }
    }
}
