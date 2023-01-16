using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Domain.Games.Elements;

namespace Application.Users.Commands
{
    public class RegisterUserCommand : IRequest<IdentityResult>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, IdentityResult>
    {
        private readonly UserManager<User> _userManager;

        public RegisterUserCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;

        }

        public async Task<IdentityResult> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            User userExists = await _userManager.FindByNameAsync(command.UserName);

            if (userExists != null)
            {
                return null;
            }

            List<PromptSet> promptSets = new List<PromptSet>();

            User user = new User
            {
                UserName = command.UserName,
                Email = command.Email,
                PromptSets = promptSets
            };

            var result = await _userManager.CreateAsync(user, command.Password);

            return result;
        }

    }
}
