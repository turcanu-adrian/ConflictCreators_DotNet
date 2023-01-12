using Application.Abstract;
using Domain.Games.Elements;
using MediatR;

namespace Application.Prompts.Commands
{
    public class AddPromptCommand : IRequest<Prompt>
    {
        public string User { get; set; } = "default";
        public string Question { get; set; } = null!;
        public string CorrectAnswer { get; set; } = null!;
        public string[] WrongAnswers { get; set; } = null!;
    }

    public class AddPromptCommandHandler : IRequestHandler<AddPromptCommand, Prompt>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddPromptCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Prompt> Handle(AddPromptCommand command, CancellationToken cancellationToken)
        {
            Prompt prompt = new Prompt
            {
                User = command.User,
                Question = command.Question,
                CorrectAnswer = command.CorrectAnswer,
                WrongAnswers = command.WrongAnswers
            };

            await _unitOfWork.PromptRepository.Add(prompt);
            await _unitOfWork.Save();

            return prompt;
        }
    }
}
