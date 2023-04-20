using Microsoft.AspNetCore.Mvc;
using ReservationService.Configuration;
using ReservationService.Core;
using ReservationService.Model;
using ReservationService.Model.DTO;
using ReservationService.Services;

namespace ReservationService.Controllers
{
    public class AccommodationController : BaseController<Accommodation>
    {
        public AccommodationController(ProjectConfiguration configuration) : base(configuration)
        {
        }

        [Route("createAccomodation")]
        [HttpPost]
        public IActionResult CreateAccommodation(AccommodationDTO dto)
        {
            AccommodationService accommodationService = new AccommodationService();

            if (dto == null)
            {
                return BadRequest();
            }

            Accommodation newAccommodation = accommodationService.CreateAccommodation(dto);

            return Ok(newAccommodation);
        }
    }
}
