using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
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

      /*  [Route("login")]
        [HttpPost]
        public IActionResult Login(LoginDTO login)
        {

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
        } */
      [HttpPost("login")]
      [AllowAnonymous]
      public async Task<IActionResult> Login(LoginDTO dto)
      {
          try
          {
              User user = _userService.Login(dto.Email, dto.Password);

              if (user == null)
              {
                  return NotFound(new { message = "Korisnik nije pronadjen" });
              }
              //ovde uradi generisanje tokena
              string tokenJwt = TokenJwt(user);
              return Ok(new {JwtToken = tokenJwt});
          }
          catch (Exception e)
          {
              return BadRequest(new {message = "Niste uneli sve potrebne podatke"});
          }
      }
      
      
      private string TokenJwt(User user)
      {
          var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])); //pristupamo kljucu
          var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); //HmacSha256 alg sa sifrovanje
          var claims = new[]
          {
              new Claim(ClaimTypes.NameIdentifier, user.Email),
              new Claim(ClaimTypes.Role, user.UserType.ToString())
          };

          var tokenJwt = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"],
              claims, expires:DateTime.Now.AddMinutes(30), signingCredentials:credentials);

          var token =  new JwtSecurityTokenHandler().WriteToken(tokenJwt);
          return token;
      } 


    }
}
