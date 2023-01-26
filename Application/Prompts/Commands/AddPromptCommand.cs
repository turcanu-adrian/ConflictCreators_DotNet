
using Application.Abstract;
using Domain;
using Domain.Games.Elements;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Prompts.Commands
{
    public class AddPromptCommand : IRequest<bool>
    {
        public string PromptSetId { get; set; }
        public string Question { get; set; } = null!;
        public string CorrectAnswer { get; set; } = null!;
        public string[] WrongAnswers { get; set; } = null!;
    }

    public class AddPromptCommandHandler : IRequestHandler<AddPromptCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddPromptCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(AddPromptCommand command, CancellationToken cancellationToken)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
    }
}
