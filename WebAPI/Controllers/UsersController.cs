using Domain;
using Domain.Games.Elements;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager = null;

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName)
                };

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECURITY_KEY")));

                var token = new JwtSecurityToken(
                    issuer: "https://localhost:7242",
                    claims: authClaims,
                    expires: DateTime.Now.AddHours(3),
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(string userName, string password)
        {
            var userExists = await _userManager.FindByNameAsync(userName);

            if (userExists != null)
            {
                return BadRequest("User already exists");
            }

            List<Prompt> prompts = new List<Prompt>();

            User user = new User
            {
                UserName = userName,
                Prompts = prompts
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                return BadRequest("Failed to create user");
            }

            return Ok("User created successfully");
        }
    }
}
