using Microsoft.AspNetCore.Mvc;
using ReservationService.Configuration;
using ReservationService.Model;

namespace ReservationService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : BaseController<Reservation>
    {
        public ReservationController(ProjectConfiguration configuration) : base(configuration)
        {
        }
    }
}
