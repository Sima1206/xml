using FlightService.Configuration;
using FlightService.Model;
using FlightService.Model.dto;
using FlightService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

namespace FlightService.Controllers
{
        [ApiController]
        [Route("api/[controller]")]
        public class AuthController : BaseController<User>
        {
            private readonly IUserService _userService;
            private readonly IConfiguration _configuration;

            public AuthController(IConfiguration configuration, IUserService userService) : base(configuration, userService)
            {
                _configuration = configuration;
                _userService = userService;
            }

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
