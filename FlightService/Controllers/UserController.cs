using FlightService.Configuration;
using FlightService.Model;
using FlightService.Model.dto;
using FlightService.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace FlightService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseController<User>
    {
        private readonly IUserService _userService;
        private readonly ProjectConfiguration _configuration;

        public UserController(ProjectConfiguration configuration, IUserService userService) : base(configuration, userService)
        {
            _configuration = configuration;
            _userService = userService;
        }


        [Route("registration")]
        [HttpPost]
        public IActionResult Registration(RegistrationDTO registration)
        {
            User user = _userService.Registration(registration.Email, registration.Password, registration.Name, registration.Surname, registration.SSN, registration.PhoneNumber);

            if (user == null)
            {
                return BadRequest("Registration is not successful.");
            }

            return Ok(user);
        }
    }
}
