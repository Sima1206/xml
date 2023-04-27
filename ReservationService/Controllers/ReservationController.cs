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






        
    }
}
