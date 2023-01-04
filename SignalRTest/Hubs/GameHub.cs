using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;
using Application.Prompts.Commands;
using Domain.Games.Elements;
using Application.Games.Commands;
using Application.Abstract;
using Application.Games.Queries;
using Application.Games.Responses;

namespace SignalRTest.Hubs
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
                    playerId = Context.ConnectionId,
                    gameId = gameId
                });

                await base.OnDisconnectedAsync(exception);
                await GetGame(result);
            }
        }

        public async Task CreateNewGame(String nickname)
        {
            Console.WriteLine("Creating New Game");

            String gameId = await _mediator.Send(new CreateGameCommand
            {
                HostName = nickname,
                HostConnectionId = Context.ConnectionId
            });

            await Groups.AddToGroupAsync(Context.ConnectionId, gameId);
            await Clients.Caller.JoinedGameAs("host");
            await GetGame(gameId);
        }

        public async Task JoinGame(String name, String gameId)
        {
            String result = await _mediator.Send(new AddPlayerCommand
            {
                gameId = gameId,
                nickname = name,
                connectionId = Context.ConnectionId
            });

            await Groups.AddToGroupAsync(Context.ConnectionId, gameId);
            await Clients.Caller.JoinedGameAs(result);
            await GetGame(gameId);
        }

        public async Task GetGame(String id)
        {
            GameResponse game = await _mediator.Send(new GetGameByIdQuery
            {
                Id = id
            });

            await Clients.Group(id).ReceiveGameState(JsonSerializer.Serialize(game));
        }

        public async Task StartGame(String id)
        {
            Prompt result = await _mediator.Send(new AddPromptCommand
            {
                Question = "Who am I talking to?",
                CorrectAnswer = "LULE",
                WrongAnswers = new[] { "IDK", "LMAO", "LULW" }
            });

            Prompt otherResult = await _mediator.Send(new AddPromptCommand
            {
                User = "Gusky",
                Question = "To whom am I talking LULE",
                CorrectAnswer = "forsen",
                WrongAnswers = new[] { "xqc", "ice", "nem" }
            });

            String gameId = await _mediator.Send(new StartGameCommand
            {
                gameId = id
            });

            await GetGame(gameId);
        }

        public async Task SendPlayerAnswer(String answer, String gameId)
        {
            (string, string) result = await _mediator.Send(new SetPlayerAnswerCommand
            {
                answer = answer,
                playerId = Context.ConnectionId,
                gameId = gameId
            });

            if (result.Item1 == "host")
            {
                await Task.Delay(4000);
                await Clients.Caller.ReceiveRightAnswer(result.Item2);
                await Task.Delay(8000);
            }

            await GetGame(gameId);
        }


    }
}
