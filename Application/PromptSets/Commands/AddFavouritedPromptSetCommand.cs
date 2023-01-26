using Application.Abstract;
using Domain;
using Domain.Games.Elements;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.PromptSets.Commands
{
    public class AddFavouritedPromptSetCommand : IRequest<bool>
    {
        public string UserId { get; set; }
        public string PromptSetId { get; set; }
    }

    public class AddFavouritedPromptSetCommandHandler : IRequestHandler<AddFavouritedPromptSetCommand, bool>
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public AddFavouritedPromptSetCommandHandler(UserManager<User> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(AddFavouritedPromptSetCommand command, CancellationToken cancellationToken)
        {
            User user = await _userManager.Users.Include(u => u.FavouritePromptSets).FirstOrDefaultAsync(u => u.Id == command.UserId);
            
            if (user != null)
            {
                PromptSet promptSet = await _unitOfWork.PromptSetRepository.GetById(command.PromptSetId);
                if (promptSet != null)
                {
                    user.FavouritePromptSets.Add(promptSet);
                    await _userManager.UpdateAsync(user);
                    return true;
                }
            }

            return false;
        }
    }
}
