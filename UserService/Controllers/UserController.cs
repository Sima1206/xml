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

        [Route("updateProfile")]
        [HttpPut]
        public IActionResult UpdateProfile(User dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            User updatedUser = _userService.UpdateProfile(dto);

            return Ok(updatedUser);
        }
        
        [Route("deleteGuestAccount/{id}")]
        [HttpPut]
        public IActionResult DeleteGuestAccount(long id)
        {
            var deleted = _userService.DeleteGuestAccount(id);
            if (deleted == true)
            {
                return Ok();
            }
            return BadRequest();
            
        }
        
        [Route("deleteHostAccount/{id}")]
        [HttpPut]
        public IActionResult DeleteHostAccount(long id)
        {
            var deleted = _userService.DeleteHostAccount(id);
            if (deleted == true)
            {
                return Ok();
            }
            return BadRequest();
            
        }
        [Route("getUserByID/{id}")]
        [HttpGet]
        public IActionResult GetUserByID(long id)
        {
            User user = _userService.GetUserByID(id);

            if (user == null)
            {
                return BadRequest();
            }

            return Ok(user);
        }
        
        [Route("updatee")]
        [HttpPut]
        public IActionResult Updatee(UserProfileDTO dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            User updatedUser = _userService.Updatee(dto);

            return Ok(updatedUser);
        }
    }
}
    
    
