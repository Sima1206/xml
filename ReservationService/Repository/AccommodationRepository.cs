using Microsoft.EntityFrameworkCore;
using ReservationService.Core;
using ReservationService.Model;

namespace ReservationService.Repository
{
    public class AccommodationRepository : BaseRepository<Accommodation>, IAccommodationRepository
    {
        public AccommodationRepository(DbContext context) : base(context)
        {
        }
    }
}
