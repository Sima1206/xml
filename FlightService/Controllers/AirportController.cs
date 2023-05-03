using FlightService.Configuration;
using FlightService.Model;
using FlightService.Model.dto;
using FlightService.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AirportController : BaseController<Airport>
    {
        private readonly IAirportService _airportService;

        public AirportController(ProjectConfiguration configuration, IUserService userService, IAirportService airportService) : base(configuration, userService)
        {
            _airportService = airportService;
        }


        [Route("all")]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_airportService.GetAll());
        }

        [Route("/{id}")]
        [HttpGet]
        public IActionResult Get(string id)
        {
            return Ok(_airportService.Get(id));
        }

        [Route("/{id}")]
        [HttpGet]
        public IActionResult Delete(string id)
        {
            return Ok(_airportService.Delete(id));
        }

        [Route("edit")]
        [HttpGet]
        public IActionResult Edit(AirportDTO airportDTO)
        {
            return Ok(_airportService.Edit(airportDTO));
        }
    }
}
