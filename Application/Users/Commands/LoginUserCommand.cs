using Application.Abstract;
using MediatR;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Domain;

namespace Application.Users.Commands
{
    public class LoginUserCommand : IRequest<JwtSecurityToken>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, JwtSecurityToken>
    {
        private readonly UserManager<User> _userManager;

        public LoginUserCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<JwtSecurityToken> Handle(LoginUserCommand command, CancellationToken cancellationToken)
        {
            User user = await _userManager.FindByNameAsync(command.UserName);

            if (user != null && await _userManager.CheckPasswordAsync(user, command.Password))
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id)
                };

                SymmetricSecurityKey authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ee442f33-e195-4896-85b7-f6ce18bfdcab"));

                JwtSecurityToken token = new JwtSecurityToken(
                    issuer: "https://localhost:7242",
                    claims: authClaims,
                    expires: DateTime.Now.AddHours(3),
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return token;
            }

            return null;
        }
    }
}
