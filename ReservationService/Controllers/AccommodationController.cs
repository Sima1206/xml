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
            var accommodationService = new AccommodationService();

            if (dto == null)
            {
                return BadRequest();
            }

            var newAccommodation = accommodationService.CreateAccommodation(dto);

            return Ok(newAccommodation);
        }
        
        [HttpGet("searchAccomodationByGuests/guestsNum/{guestsNum}")]
        public IActionResult SearchByGuestsNum(int guestsNum)
        {
            AccommodationService accommodationService = new AccommodationService();

            IEnumerable<Accommodation> searchedAccommodations = accommodationService.SearchByGuestsNum(guestsNum);

            return Ok(searchedAccommodations);
        }

        [HttpGet("searchAccomodationByLocation/location/{location}")]
        public IActionResult SearchByGuestsNum(long location)
        {
            AccommodationService accommodationService = new AccommodationService();

            IEnumerable<Accommodation> searchedAccommodations = accommodationService.SearchByLocation(location);

            return Ok(searchedAccommodations);
        }

        [HttpGet("searchAccomodationByDate/date/{startDate}/{endDate}")]
        public IActionResult SearchByDate(DateTime startDate, DateTime endDate)
        {
            AccommodationService accommodationService = new AccommodationService();

            IEnumerable<Reservation> searchedAccommodations = accommodationService.SearchByDate(startDate, endDate);

            return Ok(searchedAccommodations);
        }
        
        [Route("deleteAccommodationsByHostId{id}")]
        [HttpPut]
        public IActionResult DeleteAccommodationsByHostId(long id)
        {
            var accommodationService = new AccommodationService();
            var deleted = accommodationService.DeleteAccommodationsByHostId(id);
            if (deleted == true)
            {
                return Ok();
            }
            return BadRequest();
            
        }


        [Route("all")]
        [HttpGet]
        public IActionResult GetAll()
        {
            var accommodationService = new AccommodationService();
            var reservations = accommodationService.GetAll();
            return Ok(reservations);
        }

        [Route("host{id}")]
        [HttpGet]
        public IActionResult GetByHostId(long id)
        {
            var accommodationService = new AccommodationService();
            var reservations = accommodationService.GetByHostId(id);
            return Ok(reservations);
        }

        [HttpGet("accommodation{id}")]
        public IActionResult GetById(long id)
        {
            var accommodationService = new AccommodationService();
            var reservations = accommodationService.GetById(id);
            if (reservations is null)
            {
                return NotFound();
            }

            return Ok(reservations);
        }
        
        [HttpGet("search/{guestsNum}/{location}/{startDate}/{endDate}")]
        public IActionResult Search(int guestsNum,long location, DateTime startDate, DateTime endDate)
        {
            IReservationService reservationService = new Services.ReservationService();
            ProjectConfiguration projectConfiguration = new ProjectConfiguration(); 
            
            AccommodationService accommodationService = new AccommodationService(projectConfiguration, reservationService);
            
            
            // AccommodationService accommodationService = new AccommodationService();

            IEnumerable<Accommodation> searchedAccommodations = accommodationService.Search(guestsNum,location,startDate, endDate);

            return Ok(searchedAccommodations);
        }

    }
}
