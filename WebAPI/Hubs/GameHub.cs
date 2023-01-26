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
using Microsoft.AspNetCore.Identity;
using Domain;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Application.PromptSets.Queries;
using Domain.Games;

namespace WebAPI.Hubs
{
    [Authorize]
    [AllowAnonymous]
    public class GameHub : Hub<IGameClient>, IGameHub
    {
        private readonly IMediator _mediator;
        private readonly UserManager<User> _userManager;

        public GameHub(IMediator mediator, UserManager<User> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        public async Task CheckPlayerStatus()
        {
            string userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId != null)
            {
                string gameId = await _mediator.Send(new GetGameIdByPlayerIdQuery
                {
                    PlayerId = userId
                });

                if (gameId != null)
                {
                    GameResponse game = await _mediator.Send(new GetGameByIdQuery
                    {
                        Id = gameId
                    });

                    if (game != null && game.CurrentPhase != GamePhase.gameover)
                    {
                        await Groups.AddToGroupAsync(Context.ConnectionId, gameId);

                        PlayerType playerType = await _mediator.Send(new GetPlayerTypeByGameIdQuery
                        {
                            GameId = gameId,
                            PlayerId = userId
                        });

                        await Clients.Caller.JoinedGameAs(JsonConvert.SerializeObject(playerType, new Newtonsoft.Json.Converters.StringEnumConverter()));
                        await GetGame(gameId);
                    }
                }
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            string userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            string gameId = await _mediator.Send(new GetGameIdByPlayerIdQuery
            {
                PlayerId = Context.ConnectionId
            });

            if (gameId != null && userId == null)
            {
                bool result = await _mediator.Send(new RemovePlayerCommand
                {
                    PlayerId = Context.ConnectionId,
                    GameId = gameId
                });

                if (result)
                {
                    await base.OnDisconnectedAsync(exception);
                    await GetGame(gameId);
                }
            }
        }

        public async Task CreateNewGame(string nickname, GameType gameType, int maxGuestPlayers, string promptsSetId)
        {
            string userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            User user = await _userManager.FindByIdAsync(userId);

            string hostName = user == null ? nickname : user.DisplayName;
            Avatar hostAvatar = user != null ? user.currentAvatar : Avatar.LULE;
            List<Badge> badges = user != null ? user.Badges.ToList() : new List<Badge>();

            string gameId = await _mediator.Send(new CreateGameCommand
            {
                HostName = hostName,
                HostAvatar = hostAvatar,
                HostBadges = badges,
                HostConnectionId = userId ?? Context.ConnectionId,
                GameType = gameType,
                MaxGuestPlayers = maxGuestPlayers,
                PromptSetId = promptsSetId
            });

            await Groups.AddToGroupAsync(Context.ConnectionId, gameId);
            await Clients.Caller.JoinedGameAs(JsonConvert.SerializeObject(PlayerType.host, new Newtonsoft.Json.Converters.StringEnumConverter()));
            await GetGame(gameId);
        }

        public async Task JoinGame(string name, string gameId)
        {
            string userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            User user = await _userManager.FindByIdAsync(userId);

            string playerId = userId ?? Context.ConnectionId;
            
            string currentGameId = await _mediator.Send(new GetGameIdByPlayerIdQuery
                {
                    PlayerId = playerId
            });

            List<Badge> badges = user != null ? user.Badges.ToList() : new List<Badge>();

            if (currentGameId == null)
            {
                PlayerType result = await _mediator.Send(new AddPlayerCommand
                {
                    GameId = gameId,
                    Nickname = name,
                    ConnectionId = playerId,
                    Avatar = user != null ? user.currentAvatar : Avatar.LULE,
                    Badges = badges
                });

                if (result != PlayerType.none)
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, gameId);
                    await Clients.Caller.JoinedGameAs(JsonConvert.SerializeObject(result, new Newtonsoft.Json.Converters.StringEnumConverter()));
                    await GetGame(gameId);
                }
            }
        }

        public async Task GetGame(string id)
        {
            GameResponse game = await _mediator.Send(new GetGameByIdQuery
            {
                Id = id
            });

            if (game != null)
                if (game.Type == GameType.WWTBAM)
                    await Clients.Group(id).ReceiveGameState(JsonConvert.SerializeObject(game as WWTBAMResponse, new Newtonsoft.Json.Converters.StringEnumConverter()));
        }

        public async Task SendPlayerAnswer(string answer, string gameId)
        {
            string userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            string playerId = userId ?? Context.ConnectionId;

            PlayerType playerType = await _mediator.Send(new SetPlayerAnswerCommand
            {
                Answer = answer,
                PlayerId = playerId,
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

        public async Task EndGame(string gameId)
        {
            string userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            await UpdatePlayerPoints(gameId);

            GameResponse game = await _mediator.Send(new GetGameByIdQuery {Id= gameId});

            User user = await _userManager.FindByIdAsync(userId);

            if (user != null && (game as WWTBAMResponse).CurrentTier == 14)
            {
                TimeSpan gameDuration = DateTime.Now - game.StartTime;
                
                if ((gameDuration < user.FastestRun) || (user.FastestRun == TimeSpan.Zero))
                {
                    user.FastestRun = gameDuration;
                    await _userManager.UpdateAsync(user);
                }
            }

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

        private async Task UpdatePlayerPoints(string gameId)
        {
            GameResponse game = await _mediator.Send(new GetGameByIdQuery
            {
                Id = gameId
            });

            User hostPlayerUser = await _userManager.FindByIdAsync(game.HostPlayer.Id);

            if (hostPlayerUser != null)
            {
                hostPlayerUser.AchievementPoints += game.HostPlayer.Points;
                await _userManager.UpdateAsync(hostPlayerUser);
            }

            game.GuestPlayers.ForEach(async guestPlayer =>
            {
                User guestPlayerUser = await _userManager.FindByIdAsync(guestPlayer.Id);

                if (guestPlayerUser != null)
                {
                    guestPlayerUser.AchievementPoints += guestPlayer.Points;
                    await _userManager.UpdateAsync(guestPlayerUser);
                }
            });

        }

        public async Task<string> GetAllGames(string searchValue, bool showFull, bool showEmpty)
        {
            var result = await _mediator.Send(new GetGamesQuery
            {
                SearchValue= searchValue,
                GetFull = showFull,
                GetEmpty = showEmpty
            });

            if (result != null)
                return JsonConvert.SerializeObject(result, new Newtonsoft.Json.Converters.StringEnumConverter());

            return JsonConvert.SerializeObject(new WWTBAMResponse[0], new Newtonsoft.Json.Converters.StringEnumConverter());
        }

        public async Task LeaveGame()
        {
            string userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            string playerId = userId ?? Context.ConnectionId;

            string gameId = await _mediator.Send(new GetGameIdByPlayerIdQuery
            {
                PlayerId = playerId
            });

            if (gameId != null)
            {
                bool result = await _mediator.Send(new RemovePlayerCommand {
                    GameId = gameId, 
                    PlayerId = playerId 
                });

                if (result)
                {
                    await Groups.RemoveFromGroupAsync(Context.ConnectionId, gameId);
                    await GetGame(gameId);
                }
            }
        }
    }
}
