using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserService.Configuration;
using UserService.Core;
using UserService.Model;
using UserService.Model.DTO;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : BaseController<User>
    {
        private readonly IUserService _userService;
        private readonly ProjectConfiguration _configuration;

        public AuthController(IUserService userService, ProjectConfiguration configuration) : base(configuration, userService)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [Route("login")]
        [HttpPost]
        public IActionResult Login(LoginDTO login)
        {
            if (login.ClientID != _configuration.ClientID || login.ClientSecret != _configuration.ClientSecret)
            {
                return BadRequest("ClientID or ClientSecret was not correct, please check again");
            }

            if (login == null || login.Email == null || login.Password == null) 
            { 
                return BadRequest("Invalid client request");
            }

            User user = _userService.GetUserWithEmail(login.Email);

            if (user == null || !user.Enabled || !BCrypt.Net.BCrypt.Verify(login.Password, user.Password)) 
            {
                return BadRequest("Invalid credentials");
            }

            Claim[] claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration.Jwt.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("Id", user.Id.ToString()),
                new Claim("Email", user.Email)
            };

            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_configuration.Jwt.Key));
            SigningCredentials signIn = new(key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new(_configuration.Jwt.Issuer, _configuration.Jwt.Audience, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }

    }
}
