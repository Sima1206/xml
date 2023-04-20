using ReservationService.Configuration;
using ReservationService.Model;

namespace ReservationService.Controllers
{
    public class AccommodationController : BaseController<Accommodation>
    {
        public AccommodationController(ProjectConfiguration configuration) : base(configuration)
        {
        }
    }
}
