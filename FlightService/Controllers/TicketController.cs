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
    public class TicketController : BaseController<Ticket>
    {
        private readonly ITicketService _ticketService;

        public TicketController(ProjectConfiguration configuration, IUserService userService, ITicketService ticketService) : base(configuration, userService)
        {
            _ticketService = ticketService;
        }


        [Route("all")]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_ticketService.GetAll());
        }

        [Route("/{id}")]
        [HttpGet]
        public IActionResult Get(string id)
        {
            return Ok(_ticketService.Get(id));
        }

        [Route("/{id}")]
        [HttpGet]
        public IActionResult Delete(string id)
        {
            return Ok(_ticketService.Delete(id));
        }

        [Route("edit")]
        [HttpGet]
        public IActionResult Edit(TicketDTO ticketDTO)
        {
            return Ok(_ticketService.Edit(ticketDTO));
        }
    }
}
