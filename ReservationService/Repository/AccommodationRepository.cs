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

        public IEnumerable<Accommodation> SearchByGuestsNum(int guestsNum)
        {
            return ApplicationContext.Accommodations.Where(x => guestsNum > x.MinGuests && guestsNum < x.MaxGuests).ToList();
        }

        public IEnumerable<Accommodation> SearchByLocation(long location)
        {
            return ApplicationContext.Accommodations.Where(x => location == x.LocationId).ToList();
        }
        public IEnumerable<Accommodation> Search(int guestNum, long location)
        {
            return ApplicationContext.Accommodations.Where(x => location == x.LocationId && guestNum >= x.MinGuests 
                && guestNum <= x.MaxGuests).ToList();
        }
 





    }
}
