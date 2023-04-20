using ReservationService.Configuration;
using ReservationService.Model;

namespace ReservationService.Controllers
{
    public class ReservationController : BaseController<Reservation>
    {
        public ReservationController(ProjectConfiguration configuration) : base(configuration)
        {
        }
    }
}
