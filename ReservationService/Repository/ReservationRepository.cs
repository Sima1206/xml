using Microsoft.EntityFrameworkCore;
using ReservationService.Core;
using ReservationService.Model;

namespace ReservationService.Repository
{
    public class ReservationRepository : BaseRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(DbContext context) : base(context)
        {
        }
    }
}
