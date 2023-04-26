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


        [Route("searchAccomodationByGuests")]
        [HttpGet("guestsNum/{guestsNum}")]
        public IActionResult SearchByGuestsNum(int guestsNum)
        {
            AccommodationService accommodationService = new AccommodationService();

            IEnumerable<Accommodation> searchedAccommodations = accommodationService.SearchByGuestsNum(guestsNum);

            return Ok(searchedAccommodations);
        }

        [Route("searchAccomodationByLocation")]
        [HttpGet("location/{location}")]
        public IActionResult SearchByGuestsNum(long location)
        {
            AccommodationService accommodationService = new AccommodationService();

            IEnumerable<Accommodation> searchedAccommodations = accommodationService.SearchByLocation(location);

            return Ok(searchedAccommodations);
        }

        [Route("searchAccomodationByDate")]
        [HttpGet("date/{startDate/endDate}")]
        public IActionResult SearchByDate(DateTime startDate, DateTime endDate)
        {
            AccommodationService accommodationService = new AccommodationService();

            IEnumerable<Reservation> searchedAccommodations = accommodationService.SearchByDate(startDate, endDate);

            return Ok(searchedAccommodations);
        }






    }
}
