using Application.Abstract;
using Domain;
using Domain.Games.Elements;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application
{
    public class AddPromptSetCommand : IRequest<bool>
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string[] Tags { get; set; }
    }

    public class AddPromptSetCommandHandler : IRequestHandler<AddPromptSetCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddPromptSetCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(AddPromptSetCommand command, CancellationToken cancellationToken)
        {
            try
            {
                PromptSet promptSet = new PromptSet
                {
                    CreatedByUserId = command.UserId,
                    Name = command.Name,
                    Tags = command.Tags.ToList()
                };

                await _unitOfWork.PromptSetRepository.Add(promptSet);
                await _unitOfWork.Save();
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return false;
         }
    }
}
