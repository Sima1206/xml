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
        //Stavi private readonly ApplicationContext db; Onda svuda gde je appContext pozoves db <-Dep Inj. U konstr db = Db
        public IEnumerable<Accommodation> SearchByGuestsNum(int guestsNum)
        {
            return ApplicationContext.Accommodations.Where(x => guestsNum > x.MinGuests && guestsNum < x.MaxGuests).ToList();
        }

        public IEnumerable<Accommodation> SearchByLocation(long location)
        {
            return ApplicationContext.Accommodations.Where(x => location == x.LocationId).ToList();
        }
        
        
 





    }
}
