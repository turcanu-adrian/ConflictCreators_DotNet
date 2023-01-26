using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using WebAPI.DTOs.User;
using Microsoft.AspNetCore.Authorization;
using Domain.Enums;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace WebAPI
{
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public UsersController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userDto)
        {
            User user = await _userManager.FindByNameAsync(userDto.UserName);

            if (user != null && await _userManager.CheckPasswordAsync(user, userDto.Password))
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

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    username = user.UserName
                });
            }

            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userDto)
        {
            User user = await _userManager.FindByNameAsync(userDto.UserName);

            if (user == null)
            {
                User newUser = new User
                {
                    UserName = userDto.UserName,
                    DisplayName = userDto.DisplayName,
                    Email = userDto.Email
                };

                var result = await _userManager.CreateAsync(newUser, userDto.Password);

                if (!result.Succeeded)
                    return BadRequest("Failed to create user");

                return Ok("User created successfully");
            }

            return BadRequest("User already exists");
        }

        [Authorize]
        [HttpPost]
        [Route("setAvatar/{avatar}")]
        public async Task<IActionResult> SetAvatar(Avatar avatar)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            User user = await _userManager.FindByIdAsync(userId);

            user.currentAvatar = avatar;
            
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return BadRequest("Avatar change failed");

            return Ok("Avatar changed successfully");
        }

        [AllowAnonymous]
        [Authorize]
        [HttpGet]
        [Route("getAvatar")]
        public async Task<Avatar> GetAvatar()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            User user = await _userManager.FindByIdAsync(userId);

            if (user != null)
                return user.currentAvatar;

            return Avatar.LULE;
        }

        [HttpGet]
        [Route("getLeaderboard")]
        public async Task<IEnumerable<LeaderboardEntryDto>> GetLeaderboard()
        {
            User[] topUsersByPoints = await _userManager.Users.OrderByDescending(u => u.AchievementPoints).Take(20).ToArrayAsync();

            LeaderboardEntryDto[] leaderboardEntryDto = topUsersByPoints.Select(entry => new LeaderboardEntryDto
            {
                AchievementPoints = entry.AchievementPoints,
                Name = entry.DisplayName,
                FastestGame = entry.FastestRun,
                Avatar = entry.currentAvatar
            }).ToArray();

            return leaderboardEntryDto;
        }
    }
}
