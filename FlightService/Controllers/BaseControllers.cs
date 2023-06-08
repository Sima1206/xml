using FlightService.Configuration;
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
    [Route("api/[controller")]
    public class BaseController<TEntity> : ControllerBase where TEntity : class
    {
        protected ProjectConfiguration _configuration;
        protected IUserService _userService;

        public BaseController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }
    }
}
