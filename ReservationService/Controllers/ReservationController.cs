using Microsoft.AspNetCore.Mvc;
using ReservationService.Configuration;
using ReservationService.Core;
using ReservationService.Model;
using ReservationService.Model.DTO;
using ReservationService.Services;

namespace ReservationService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : Controller
    {
        private IReservationService _reservationService;
        private readonly ProjectConfiguration _configuration;

        public ReservationController(ProjectConfiguration configuration, IReservationService reservationService)
        {
            
            _configuration = configuration;
            _reservationService = reservationService;
        }

        [Route("createReservation")]
        [HttpPost]
        public IActionResult CreateReservation(ReservationDTO dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            Reservation newReservation = _reservationService.CreateReservation(dto);

            return Ok(newReservation);
        }
        
        [Route("guestCancel")]
        [HttpDelete]
        public bool CancelReservationByGuest([FromBody] Reservation reservation)
        {
            return  _reservationService.CancelReservationByGuest(reservation);
        }

        [Route("autoAccept")]
        [HttpPut]
        public IActionResult autoAccept([FromBody] Reservation reservation)
        {
            _reservationService.AutoAcceptReservation(reservation);
            return Ok(reservation);
        }

        [Route("accept")]
        [HttpPut]
        public IActionResult accept([FromBody] Reservation reservation)
        {
            _reservationService.AcceptReservation(reservation);
            return Ok(reservation);
        }

        [Route("all")]
        [HttpGet]
        public IActionResult GetAll()
        {
            var reservations = _reservationService.GetAll();
            return Ok(reservations);
        }
        
        [HttpGet("reservation{id}")]
        public async Task<ActionResult<Reservation>> Get(long id)
        {
            var reservation =    _reservationService.GetById(id);
            return reservation ?? NotFound();
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
        
        [HttpGet("guestt{id}")]
        public IActionResult GetByGuest(long id)
        {
            ReservationService.Services.ReservationService reservationService = new ReservationService.Services.ReservationService();
            var reservations =    reservationService.GetByGuest(id);

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
        
  /*      [HttpGet("matching{id}")]
        public IActionResult GetWithMatchingPeriods(long id)
        {
           var reservations =    _reservationService.GetWithMatchingPeriods(id);

            if (reservations is null)
            {
                return NotFound();
            }

            return Ok(reservations);
        }
        [Route("updateReservation")]
        [HttpPut]
        public IActionResult UpdateReservation(Reservation dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            Reservation updatedReservation  =   _reservationService.UpdateReservation(dto);

            return Ok(updatedReservation);
        }

        [HttpDelete("delete{id}")]
        public virtual IActionResult Delete(int id)
        {
            var response = _reservationService.Delete(id);
            return Ok(response);
        } */
    } 
    
  
    
}
