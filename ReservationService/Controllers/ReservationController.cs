using AutoMapper;
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
        private readonly IMapper _mapper;
        public ReservationController(ProjectConfiguration configuration, IMapper mapper) : base(configuration)
        {
            _mapper = mapper;
        }

        [Route("createReservation")]
        [HttpPost]
        public IActionResult CreateReservation(ReservationDTO dto)
        {
            ReservationService.Services.ReservationService reservationService = new ReservationService.Services.ReservationService();
            
            if (dto == null)
            {
                return BadRequest(new {message = "Wrong reservation, please check your dates"});

            }

            Reservation newReservation = reservationService.CreateReservation(dto);

            if (newReservation == null)
            {
                return BadRequest(new {message = "Wrong reservation, please check your dates"});
            }
            
            return Ok(newReservation);
        }

        [Route("updateReservation")]
        [HttpPut]
        public IActionResult UpdateReservation(long id, ReservationDTO reservationDto)
        {
            ReservationService.Services.ReservationService reservationService = new ReservationService.Services.ReservationService();

            if (reservationDto == null)
                return BadRequest(new { message = "Wrong reservation, please check your fields"});

            Reservation reservation =
                reservationService.UpdateReservation(id, _mapper.Map<Reservation>(reservationDto), reservationDto.TermId);
            
            if(reservation == null)
                return BadRequest(new { message = "Wrong reservation, please check your fields"});

            return Ok(new {message = "Updated reservation successfully"});
        }




        
    }
}
