using Application.Abstract;
using Domain.Games.Elements;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Prompts.Commands
{
    public class RemovePromptCommand : IRequest<bool>
    {
        public string PromptId { get; set; }
    }

    public class RemovePromptCommandHandler : IRequestHandler<RemovePromptCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemovePromptCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(RemovePromptCommand command, CancellationToken cancellationToken)
        {
            Prompt prompt = await _unitOfWork.PromptRepository.GetById(command.PromptId);

            if (prompt != null)
            {
                _unitOfWork.PromptRepository.Remove(prompt);
                await _unitOfWork.Save();
                return true;
            }

            return false;
        }
    }
}
