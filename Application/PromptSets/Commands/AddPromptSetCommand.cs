using Application.Abstract;
using Domain;
using Domain.Games.Elements;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class AddPromptSetCommand : IRequest<bool>
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public List<string> Tags { get; set; }
    }

    public class AddPromptSetCommandHandler : IRequestHandler<AddPromptSetCommand, bool>
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public AddPromptSetCommandHandler(UserManager<User> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(AddPromptSetCommand command, CancellationToken cancellationToken)
        {
            User user = await _userManager.FindByIdAsync(command.UserId);
            if (user != null) 
            {
                PromptSet promptSet = new PromptSet
                {
                    UserId = command.UserId,
                    Name = command.Name,
                    Tags = command.Tags
                };
                await _unitOfWork.PromptSetRepository.Add(promptSet);
                await _unitOfWork.Save();
                return true;
            }
            return false;
         }
    }
}
