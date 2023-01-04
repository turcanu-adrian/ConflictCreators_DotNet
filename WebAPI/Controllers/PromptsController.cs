using Microsoft.AspNetCore.Mvc;
using MediatR;
using WebAPI.DTOs;

namespace WebAPI.Controllers
{
/*    [Route("api/v1/[controller]")]
    [ApiController]
    public class PromptsController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly IMediator _mediator;

        *//*private readonly MySettingsSection _settings;*//*

        public PromptsController(IMapper mapper, IMediator mediator)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddPrompt([FromBody] PromptPutPostDto prompt)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _mediator.Send(new AddPromptCommand
            {
                Question = prompt.Question,
                CorrectAnswer = prompt.CorrectAnswer,
                WrongAnswers = prompt.WrongAnswers
            });

            var mappedResult = _mapper.Map<PromptGetDto>(result);

            return CreatedAtAction(nameof(GetById), new { id = mappedResult.Id}, mappedResult);
        }

        [HttpGet]
        public async Task<IActionResult> GetPrompts()
        {
            var result = await _mediator.Send(new GetAllPromptsCommand { });
            var mappedResult = _mapper.Map<List<PromptGetDto>>(result);
            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetPromptByIdQuery { PromptId = id });

            if (result == null)
                return NotFound();

            var mappedResult = _mapper.Map<PromptGetDto>(result);
            return Ok(mappedResult);
        }
    }*/
}
