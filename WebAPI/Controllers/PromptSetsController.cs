using Application.PromptSets.Queries;
using Domain.Games.Elements;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAPI.DTOs.Prompt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Application;
using Application.PromptSets.Commands;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/promptSets")]
    public class PromptSetsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserManager<User> _userManager;

        public PromptSetsController(IMediator mediator, UserManager<User> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("getBrowse/{searchString?}")]
        public async Task<IEnumerable<PromptSetGetDto>> GetBrowseTabPromptSets(string searchString = "")
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            User user = userId == null ? null : await _userManager.Users.Include(u => u.FavouritePromptSets).FirstAsync(u => u.Id == userId);

            List<PromptSet> promptSets = await _mediator.Send(new GetAllPromptSetsQuery { });
            List<Task<PromptSetGetDto>> tasks = new();

            if (user != null)
            {
                List<PromptSet> favouritePromptSets = user.FavouritePromptSets.ToList();
                List<PromptSet> createdPromptSets = await _mediator.Send(new GetCreatedPromptSetsQuery
                {
                    UserId = userId,
                });

                tasks = promptSets.Where(ps => !favouritePromptSets.Contains(ps) && !createdPromptSets.Contains(ps)).Select(async ps =>
                {
                    User user = await _userManager.FindByIdAsync(ps.CreatedByUserId);

                    return new PromptSetGetDto
                    {
                        CreatorName = user == null ? "default" : user.DisplayName,
                        Name = ps.Name,
                        Tags = ps.Tags.ToArray(),
                        Id = ps.Id,
                        PromptsCount = ps.Prompts != null ? ps.Prompts.Count : 0
                    };
                }).ToList();
            }
            else
                tasks = promptSets.Select(async ps =>
                {
                    User user = await _userManager.FindByIdAsync(ps.CreatedByUserId);

                    return new PromptSetGetDto
                    {
                        CreatorName = user == null ? "default" : user.DisplayName,
                        Name = ps.Name,
                        Tags = ps.Tags.ToArray(),
                        Id = ps.Id,
                        PromptsCount = ps.Prompts != null ? ps.Prompts.Count : 0
                    };
                }).ToList();

            PromptSetGetDto[] promptSetDtos = await Task.WhenAll(tasks);

            return promptSetDtos.Where(ps => ps.CreatorName.ToLower().Contains(searchString) || ps.Name.ToLower().Contains(searchString) || ps.Tags.Any(tag => tag.ToLower().Contains(searchString)));
        }

        [HttpGet]
        [Route("getFavourites/{searchString?}")]
        public async Task<IEnumerable<PromptSetGetDto>> GetFavouritesTabPromptSets(string searchString = "")
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            User user = await _userManager.Users.Include(u => u.FavouritePromptSets).FirstAsync(u => u.Id == userId);

            List<Task<PromptSetGetDto>> tasks = user.FavouritePromptSets.Select(async ps =>
            {
                User user = await _userManager.FindByIdAsync(ps.CreatedByUserId);

                return new PromptSetGetDto
                {
                    CreatorName = (user != null) ? user.UserName : "default",
                    Name = ps.Name,
                    Tags = ps.Tags.ToArray(),
                    Id = ps.Id,
                    PromptsCount = ps.Prompts != null ? ps.Prompts.Count : 0
                };
            }).ToList();

            PromptSetGetDto[] promptSetsDto = await Task.WhenAll(tasks);

            return promptSetsDto.Where(ps => ps.CreatorName.ToLower().Contains(searchString) || ps.Name.ToLower().Contains(searchString) || ps.Tags.Any(tag => tag.ToLower().Contains(searchString)));
        }

        [HttpGet]
        [Route("getCreated/{searchString?}")]
        public async Task<IEnumerable<PromptSetGetDto>> GetCreatedTabPromptSets(string searchString = "") 
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            User user = await _userManager.FindByIdAsync(userId);

            var result = await _mediator.Send(new GetCreatedPromptSetsQuery
            {
                UserId = userId
            });

            List<PromptSetGetDto> promptSetsDto = result.Select(ps =>
            {
                return new PromptSetGetDto
                {
                    CreatorName = user.DisplayName,
                    Name = ps.Name,
                    Tags = ps.Tags.ToArray(),
                    Id = ps.Id,
                    PromptsCount = ps.Prompts != null ? ps.Prompts.Count : 0
                };

            }).ToList();

            return promptSetsDto.Where(ps => ps.CreatorName.ToLower().Contains(searchString) || ps.Name.ToLower().Contains(searchString) || ps.Tags.Any(tag => tag.ToLower().Contains(searchString)));
        }

        [HttpPost]
        [Route("addToCreated")]
        public async Task<IActionResult> AddToCreated([FromBody] PromptSetModel promptSet)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _mediator.Send(new AddPromptSetCommand
            {
                UserId = userId,
                Name = promptSet.Name,
                Tags = promptSet.Tags
            });

            if (result)
                return Ok("worked");

            return BadRequest("failed");
        }

        [HttpPost]
        [Route("updateCreated")]
        public async Task<IActionResult> UpdateCreated([FromBody] PromptSetModel promptSet)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            bool result = await _mediator.Send(new UpdatePromptSetCommand
            {
                PromptSetId = promptSet.Id,
                PromptSetName = promptSet.Name,
                PromptSetTags = promptSet.Tags
            });

            if (result)
                return Ok("it worked");

            return BadRequest("promptset doesn't exist");

        }

        [HttpPost]
        [Route("addToFavourites/{promptSetId}")]
        public async Task<IActionResult> AddToFavourites(string promptSetId)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            User user = await _userManager.Users.Include(u => u.FavouritePromptSets).FirstAsync(u => u.Id == userId);

            PromptSet promptSet = await _mediator.Send(new GetPromptsSetByIdQuery
            {
                Id = promptSetId
            });

            if (promptSet != null)
            {
                user.FavouritePromptSets.Add(promptSet);
                await _userManager.UpdateAsync(user);

                return Ok("added to favourites");
            }

            return BadRequest("promptset doesn't exist");
        }

        [HttpPost]
        [Route("removeFromFavourites/{promptSetId}")]
        public async Task<IActionResult> RemoveFromFavourites(string promptSetId)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            User user = await _userManager.Users.Include(u => u.FavouritePromptSets).FirstAsync(u => u.Id == userId);

            PromptSet promptSet = await _mediator.Send(new GetPromptsSetByIdQuery { Id = promptSetId });
            
            if (promptSet != null)
            {
                user.FavouritePromptSets.Remove(promptSet);
                await _userManager.UpdateAsync(user);
                return Ok("removed from favourites");
            }

            return BadRequest("prompt set not found");
        }

        [HttpPost]
        [Route("removeFromCreated/{promptSetId}")]
        public async Task<IActionResult> Remove(string promptSetId)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            User user = await _userManager.FindByIdAsync(userId);

            PromptSet promptSet = await _mediator.Send(new GetPromptsSetByIdQuery
            {
                Id = promptSetId
            });

            if (promptSet != null)
            {
                if (promptSet.CreatedByUserId == userId)
                {
                    bool result = await _mediator.Send(new RemovePromptSetCommand
                    {
                        PromptSetId = promptSetId
                    });

                    if (result)
                        return Ok("removed");

                    return BadRequest("failed to remove");
                }
                return Unauthorized("prompt set not created by user");
            }

            return BadRequest("prompt set doesn't exist");
        }
    }
}
