using Microsoft.AspNetCore.Mvc;
using ReservationService.Configuration;
using ReservationService.Model;
using ReservationService.Model.DTO;
using ReservationService.Services;

namespace ReservationService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : BaseController<Reservation>
    {
       // private Services.ReservationService _reservationService;
        public ReservationController(ProjectConfiguration configuration) : base(configuration)
        {
        }

        [Route("createReservation")]
        [HttpPost]
        public IActionResult CreateReservation(ReservationDTO dto)
        {
            ReservationService.Services.ReservationService reservationService = new ReservationService.Services.ReservationService();

            if (dto == null)
            {
                return BadRequest();
            }

            Reservation newReservation = reservationService.CreateReservation(dto);

            return Ok(newReservation);
        }
/*
        [HttpPut("accept{id}")]
        public IActionResult Update([FromRoute]int id, [FromBody]Reservation entity)
        {
            if (entity == null)
            {
                return BadRequest();
            }
            
            bool response = _baseService.Update((int)id, entity);
            ReservationService.Services.ReservationService reservationService = new ReservationService.Services.ReservationService();
            reservationService.DeleteReservationsWithMatchingPeriod(entity);
            return Ok(response);
        } */

        [Route("guestCancel")]
        [HttpDelete]
        public bool CancelReservationByGuest([FromBody] Reservation reservation)
        {
            ReservationService.Services.ReservationService reservationService = new ReservationService.Services.ReservationService();
            return  reservationService.CancelReservationByGuest(reservation);
        }

        [Route("autoAccept")]
        [HttpPut]
        public IActionResult autoAccept([FromBody] Reservation reservation)
        {
            ReservationService.Services.ReservationService reservationService = new ReservationService.Services.ReservationService(); 
            reservationService.AutoAcceptReservation(reservation);
            return Ok(reservation);
        }

        [Route("accept")]
        [HttpPut]
        public IActionResult accept([FromBody] Reservation reservation)
        {
            ReservationService.Services.ReservationService reservationService = new ReservationService.Services.ReservationService(); 
        //    reservationService.AcceptReservation(reservation);
            return Ok(reservation);
        }

//delete vec imas


        [Route("all")]
        [HttpGet]
        public IActionResult GetAll()
        {
            ReservationService.Services.ReservationService reservationService = new ReservationService.Services.ReservationService();
            var reservations = reservationService.GetAll();
            return Ok(reservations);
        }
        
        [HttpGet("reservation{id}")]
        public async Task<ActionResult<Reservation>> Get(long id)
        {
            ReservationService.Services.ReservationService reservationService = new ReservationService.Services.ReservationService();
            var reservation =    reservationService.GetById(id);

            if (reservation is null)
            {
                return NotFound();
            }

            return reservation;
        }
        [HttpGet("host{id}")]
        public IActionResult GetByHostId(long id)
        {
            ReservationService.Services.ReservationService reservationService = new ReservationService.Services.ReservationService();
            var reservations =    reservationService.GetByHostId(id);

            if (reservations is null)
            {
                return NotFound();
            }

            return Ok(reservations);
        }
        [HttpGet("accommodation{id}")]
        public IActionResult GetByAccommodation(long id)
        {
            ReservationService.Services.ReservationService reservationService = new ReservationService.Services.ReservationService();
            var reservations =    reservationService.GetByAccommodation(id);

            if (reservations is null)
            {
                return NotFound();
            }

            return Ok(reservations);
        }
    }
}
