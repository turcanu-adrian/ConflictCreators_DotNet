using Application.Abstract;
using Domain;
using Domain.Games.Elements;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Prompts.Commands
{
    public class AddPromptCommand : IRequest<bool>
    {
        public string UserId { get; set; }
        public string PromptSetId { get; set; }
        public string Question { get; set; } = null!;
        public string CorrectAnswer { get; set; } = null!;
        public string[] WrongAnswers { get; set; } = null!;
    }

    public class AddPromptCommandHandler : IRequestHandler<AddPromptCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public AddPromptCommandHandler(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<bool> Handle(AddPromptCommand command, CancellationToken cancellationToken)
        {
            User user = await _userManager.FindByIdAsync(command.UserId);
            
            if (user != null)
            {
                Prompt prompt = new Prompt
                {
                    PromptSetId = command.PromptSetId,
                    Question = command.Question,
                    CorrectAnswer = command.CorrectAnswer,
                    WrongAnswers = command.WrongAnswers
                };

                await _unitOfWork.PromptRepository.Add(prompt);
                await _unitOfWork.Save();

                return true;
            }

            return false;
        }
    }
}
