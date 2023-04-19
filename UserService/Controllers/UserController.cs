using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Configuration;
using UserService.Core;
using UserService.Model;
using UserService.Model.DTO;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseController<User>
    {
        public UserController(ProjectConfiguration configuration, IUserService userService) : base(configuration, userService)
        {
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetCurrent()
        {
            return Ok(GetCurrentUser());
        }
        [Route("register")]
        [HttpPost]
        public IActionResult Register(RegistrationDTO dto)
        {
            if (_userService.GetUserWithEmail(dto.Email) != null)
            {
                return BadRequest("Entered email already exists!");
            }

            if (dto == null)
            {
                return BadRequest("Dto is null");
            }

            User registeredUser = _userService.Register(dto);
            return Ok(registeredUser);
        }
    }
}
