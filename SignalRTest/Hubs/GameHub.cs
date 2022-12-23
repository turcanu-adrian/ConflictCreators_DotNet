using Application.Game.Commands;
using Application.Game.Queries;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Application.Game.Responses;
using System.Text.Json;
using Application.Prompts.Commands;
using Domain.Games.Elements;

namespace SignalRTest.Hubs
{
    public class GameHub : Hub
    {
        private readonly IMediator _mediator;

        public GameHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine(Context.ConnectionId + " has disconnected, removing them from game");
            String gameId = await _mediator.Send(new GetGameIdByPlayerIdQuery
            {
                PlayerId = Context.ConnectionId
            });

            String result = await _mediator.Send(new RemovePlayerCommand
            {
                playerId = Context.ConnectionId,
                gameId = gameId
            });

            await base.OnDisconnectedAsync(exception);
            await GetGame(result);
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
            await GetGame(gameId);
        }

        public async Task GetGame(String id)
        {
            GameResponse game = await _mediator.Send(new GetGameByIdQuery
            {
                Id = id
            });

            await Clients.Group(id).SendAsync("ReceiveMessage", JsonSerializer.Serialize(game));
        }

        public async Task StartGame(String id)
        {
            Prompt result = await _mediator.Send(new AddPromptCommand
            {
                Question = "Who am I talking to?",
                CorrectAnswer = "LULE",
                WrongAnswers = new[] { "IDK", "LMAO", "LULW" }
            });

            String gameId = await _mediator.Send(new StartGameCommand
            {
                gameId = id
            });

            await GetGame(gameId);
        }

        public async Task SendPlayerAnswer(String answer, String gameId)
        {
            String result = await _mediator.Send(new SetPlayerAnswerCommand
            {
                answer = answer,
                playerId = Context.ConnectionId,
                gameId = gameId
            });

            await GetGame(result);
        }

    }
}
