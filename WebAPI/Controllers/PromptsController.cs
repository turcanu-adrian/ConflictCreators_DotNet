using Microsoft.AspNetCore.Mvc;
using MediatR;
using WebAPI.DTOs;
using Microsoft.AspNetCore.Authorization;
using WebAPI.DTOs.Prompt;
using Application.Prompts.Commands;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNet.Identity;
using Application;
using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/prompts")]
    public class PromptsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PromptsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("add")]
        [Authorize]
        public async Task<IActionResult> AddPrompt([FromBody] PromptAddDto prompt)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _mediator.Send(new AddPromptCommand
            {
                PromptSetId= prompt.PromptSetId,
                UserId = userId,
                Question = prompt.Question,
                CorrectAnswer = prompt.CorrectAnswer,
                WrongAnswers = prompt.WrongAnswers
            });

            if (result)
                return Ok("worked");

            return BadRequest("failed");
        }

        [HttpPost]
        [Route("addSet")]
        [Authorize]
        public async Task<IActionResult> AddPromptSet([FromBody] PromptSetAddDto promptSet) 
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
    }
}
