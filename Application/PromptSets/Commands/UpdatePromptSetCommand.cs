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
    public class UpdatePromptSetCommand : IRequest<bool>
    {
        public string PromptSetId { get; set; }
        public string PromptSetName { get; set; }
        public string[] PromptSetTags { get; set; }
    }

    public class UpdatePromptSetCommandHandler : IRequestHandler<UpdatePromptSetCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePromptSetCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdatePromptSetCommand command, CancellationToken cancellationToken)
        {
            PromptSet promptSet = await _unitOfWork.PromptSetRepository.GetById(command.PromptSetId);

            if (promptSet != null)
            {
                promptSet.Name = command.PromptSetName;
                promptSet.Tags= command.PromptSetTags;
                await _unitOfWork.PromptSetRepository.Update(promptSet);
                await _unitOfWork.Save();
                return true;
            }

            return false;
        }
    }
}
