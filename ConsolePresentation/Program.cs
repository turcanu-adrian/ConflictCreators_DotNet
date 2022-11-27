using Application;
using Application.Games.Commands.CreateGame;
using Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using GameDto = Application.Games.Commands.CreateGame.GameDto;

var diContainer = new ServiceCollection()
    .AddScoped<IGameRepository, InMemoryGameRepository>()
    .AddMediatR(typeof(IGameRepository))
    .BuildServiceProvider();

var mediator = diContainer.GetRequiredService<IMediator>();

var test = diContainer.GetRequiredService<IGameRepository>();


var gameId = await mediator.Send(new CreateGameCommand
{
    HostUsername = "Gusky",
    Game = new GameDto
    {
        gamemode = "WWTBAM",
        hostPlayerName = "Gusky"
    }
});

Console.WriteLine(test.GetGame(gameId).HostPlayer.Nickname);