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
    public class FlightController : BaseController<Flight>
    {
        private readonly IFlightService _flightService;

        public FlightController(ProjectConfiguration configuration, IUserService userService, IFlightService flightService) : base(configuration, userService)
        {
            _flightService = flightService;
        }


        [Route("all")]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_flightService.GetAll());
        }

        [Route("/{id}")]
        [HttpGet]
        public IActionResult Get(string id)
        {
            return Ok(_flightService.Get(id));
        }

        [Route("/{id}")]
        [HttpGet]
        public IActionResult Delete(string id)
        {
            return Ok(_flightService.Delete(id));
        }

        [Route("edit")]
        [HttpGet]
        public IActionResult Edit(FlightDTO flightDTO)
        {
            return Ok(_flightService.Edit(flightDTO));
        }

        [Route("addflight")]
        [HttpPost]
        public IActionResult Registration(FlightDTO flightdto)
        {
            Flight flight = _flightService.Create(flightdto.AirportDestination, flightdto.AirportDeparture, flightdto.DepartureTime, flightdto.ArrivalTime, flightdto.Duration, flightdto.TicketPrice, flightdto.Capacity);

            if (flight == null)
            {
                return BadRequest("Registration is not successful.");
            }

            return Ok(flight);
        }
    }
}
