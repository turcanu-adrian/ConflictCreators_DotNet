using MediatR;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Application.Abstract;
using Application.Games.Base.Queries;
using Application.Games.Base.Commands;
using Application.Games.Base.Responses;
using Domain.Enums;
using Application.Games.WWTBAM.Responses;
using Application.Games.WWTBAM.Commands;
using Application.Prompts.Commands;

namespace WebAPI.Hubs
{
    public class GameHub : Hub<IGameClient>, IGameHub
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

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine(Context.ConnectionId + " has disconnected, removing them from game");

            String gameId = await _mediator.Send(new GetGameIdByPlayerIdQuery
            {
                PlayerId = Context.ConnectionId
            });

            if (gameId != "nogamefound")
            {
                String result = await _mediator.Send(new RemovePlayerCommand
                {
                    PlayerId = Context.ConnectionId,
                    GameId = gameId
                });

                await base.OnDisconnectedAsync(exception);
                await GetGame(result);
            }
        }

        public async Task CreateNewGame(string nickname)
        {
            string gameId = await _mediator.Send(new CreateGameCommand
            {
                HostName = nickname,
                HostConnectionId = Context.ConnectionId,
                GameType = "WWTBAM",
                promptSetId = new List<string> { "97d2cfc1a93646a990abf53166da7630" }
            });

            await Groups.AddToGroupAsync(Context.ConnectionId, gameId);
            await Clients.Caller.JoinedGameAs(JsonConvert.SerializeObject(PlayerType.host, new Newtonsoft.Json.Converters.StringEnumConverter()));
            await GetGame(gameId);
        }

        public async Task JoinGame(String name, String gameId)
        {
            Console.WriteLine("new player joined game");

            PlayerType result = await _mediator.Send(new AddPlayerCommand
            {
                GameId = gameId,
                Nickname = name,
                ConnectionId = Context.ConnectionId
            });

            if (result != PlayerType.none)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, gameId);
                await Clients.Caller.JoinedGameAs(JsonConvert.SerializeObject(result, new Newtonsoft.Json.Converters.StringEnumConverter()));
                await GetGame(gameId);
            }
        }

        public async Task GetGame(String id)
        {
            GameResponse game = await _mediator.Send(new GetGameByIdQuery
            {
                Id = id
            });

            if (game != null)
                if (game.Type == GameType.WWTBAM)
                    await Clients.Group(id).ReceiveGameState(JsonConvert.SerializeObject(game as WWTBAMResponse, new Newtonsoft.Json.Converters.StringEnumConverter()));
        }

        public async Task SendPlayerAnswer(String answer, String gameId)
        {
            PlayerType playerType = await _mediator.Send(new SetPlayerAnswerCommand
            {
                Answer = answer,
                PlayerId = Context.ConnectionId,
                GameId = gameId
            });

            if (playerType == PlayerType.host)
            {
                string rightAnswer = await _mediator.Send(new CheckAnswerCommand
                {
                    GameId = gameId
                });
                await Clients.Caller.ReceiveRightAnswer(rightAnswer);
            }

            await GetGame(gameId);
        }

        public async Task EndGame(String gameId)
        {
            await _mediator.Send(new EndGameCommand
            {
                GameId = gameId
            });

            await GetGame(gameId);
        }

        public async Task ContinueGame(string gameId)
        {
            await _mediator.Send(new ContinueGameCommand
            {
                GameId = gameId
            });

            await GetGame(gameId);
        }

        public async Task UseCheat(string gameId, Cheat cheat)
        {
            bool result = await _mediator.Send(new UseCheatCommand
            {
                GameId = gameId,
                Cheat = cheat
            });

            if (result)
                await GetGame(gameId);
        }

    }
}
