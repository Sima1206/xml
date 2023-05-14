using Microsoft.AspNetCore.Mvc;
using ReservationService.Configuration;
using ReservationService.Core;
using ReservationService.Model;
using ReservationService.Model.DTO;

namespace ReservationService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly ProjectConfiguration _configuration;

        public ReservationController(ProjectConfiguration configuration, IReservationService reservationService)
        {

            _configuration = configuration;
            _reservationService = reservationService;
        }

        [Route("createReservation")]
        [HttpPost]
        public IActionResult CreateReservation(ReservationDTO? dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }
            var newReservation = _reservationService.CreateReservation(dto);
            return Ok(newReservation);
        }

        [Route("guestCancel")]
        [HttpPut]
        public bool CancelReservationByGuest([FromBody] Reservation reservation)
        {
            return _reservationService.CancelReservationByGuest(reservation);
        }

        [Route("accept")]
        [HttpPut]
        public IActionResult Accept([FromBody] Reservation reservation)
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

        [HttpGet("reservation{id:long}")]
        public Task<ActionResult<Reservation>> Get(long id)
        {
            var reservation = _reservationService.GetById(id);
            return Task.FromResult(reservation);
        }

        [HttpGet("host{id:long}")]
        public IActionResult GetByHostId(long id)
        {
           var reservations = _reservationService.GetByHostId(id);
           if (reservations is null)
           {
               return NotFound();
           }
           return Ok(reservations);
        }

        [HttpGet("guest{id}")]
        public IActionResult GetByGuestId(long id)
        {
            var reservations = _reservationService.GetByGuestId(id);


            if (reservations is null)
            {
                return NotFound();
            }

            return Ok(reservations);
        }

        [HttpGet("accommodation{id}")]
        public IActionResult GetByAccommodation(long id)
        {
            var reservations = _reservationService.GetByAccommodationId(id);
            return Ok(reservations);
        }


        [HttpGet("notAvailableDates{id}")]
        public IActionResult GetNonAvailableDates(long id)
        {
            var dates = _reservationService.IsAccommodationAvailable(id);

            if (dates is null)
            {
                return NotFound();
            }

            return Ok(dates);
        }
        
        [HttpGet("totalPrice")]
        public IActionResult TotalPrice(Reservation dto)
        {
            var price = _reservationService.TotalPrice(dto);
            return Ok(price);
        }
    }

}
