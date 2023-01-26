using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using WebAPI.DTOs.Prompt;
using Application.Prompts.Commands;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Domain.Games.Elements;
using Domain;
using Application.Prompts.Queries;
using Application.PromptSets.Queries;
using Application.PromptSets.Commands;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/prompts")]
    public class PromptsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserManager<User> _userManager;

        public PromptsController(IMediator mediator, UserManager<User> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddPrompt([FromBody] PromptAddDto prompt)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _mediator.Send(new AddPromptCommand
            {
                PromptSetId= prompt.PromptSetId,
                Question = prompt.Question,
                CorrectAnswer = prompt.CorrectAnswer,
                WrongAnswers = prompt.WrongAnswers
            });

            if (result)
                return Ok("worked");

            return BadRequest("failed");
        }

        [HttpPost]
        [Route("remove/{promptId}")]
        public async Task<IActionResult> RemovePrompt(string promptId)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _mediator.Send(new RemovePromptCommand
            {
                PromptId = promptId
            });

            if (result)
                return Ok("removed");

            return BadRequest("prompt doesn't exist");
        }

        [HttpGet("getBySet/{promptSetId}")]
        public async Task<IEnumerable<PromptGetDtoBase>> GetAllPromptsByPromptSetId(string promptSetId)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            List<Prompt> prompts = await _mediator.Send(new GetPromptsBySetIdQuery
            {
                PromptSetId = promptSetId
            });

            PromptSet promptSet = await _mediator.Send(new GetPromptsSetByIdQuery
            {
                Id = promptSetId
            });

            if (promptSet != null && promptSet.CreatedByUserId == userId)
            {
                List<CreatorPromptGetDto> creatorPromptsDto = prompts.Select(p => new CreatorPromptGetDto
                {
                    Id = p.Id,
                    Question = p.Question,
                    CorrectAnswer = p.CorrectAnswer,
                    WrongAnswers = p.WrongAnswers
                }).ToList();

                return creatorPromptsDto;
            }

            List<PromptGetDto> promptsDto = prompts.Select(p => new PromptGetDto
            {
                Id = p.Id,
                Question = p.Question
            }).ToList();

            return promptsDto;
        }

    }
}
