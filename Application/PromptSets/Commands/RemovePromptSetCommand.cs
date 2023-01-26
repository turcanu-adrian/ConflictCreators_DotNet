using Application.Abstract;
using Domain.Games.Elements;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PromptSets.Commands
{
    public class RemovePromptSetCommand : IRequest<bool>
    {
        public string PromptSetId { get; set; }
    }

    public class RemovePromptSetCommandHandler : IRequestHandler<RemovePromptSetCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemovePromptSetCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(RemovePromptSetCommand command, CancellationToken cancellationToken)
        {
            try
            {
                PromptSet promptSet = await _unitOfWork.PromptSetRepository.GetById(command.PromptSetId);

                _unitOfWork.PromptSetRepository.Remove(promptSet);
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
