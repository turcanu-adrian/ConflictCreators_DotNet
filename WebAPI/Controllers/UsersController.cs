using Domain;
using Domain.Games.Elements;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using MediatR;
using Application.Users.Commands;
using WebAPI.DTOs.User;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI
{
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userDto)
        {
            Console.WriteLine(userDto.UserName);
            var result = await _mediator.Send(new LoginUserCommand
            {
                UserName = userDto.UserName,
                Password = userDto.Password
            });

            if (result!=null)
            {
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(result),
                    expiration = result.ValidTo,
                    username = userDto.UserName
                });
            }

            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userDto)
        {
            Console.WriteLine("New register request with parameters " + userDto.UserName + " " + userDto.Email + " " + userDto.Password);
            IdentityResult result = await _mediator.Send(new RegisterUserCommand
            {
                UserName = userDto.UserName,
                Email = userDto.Email,
                Password = userDto.Password
            });

            if (result == null)
            {
                return BadRequest("User already exists");
            }

            if (!result.Succeeded)
            {
                return BadRequest("Failed to create user");
            }

            return Ok("User created successfully");
        }
    }
}
