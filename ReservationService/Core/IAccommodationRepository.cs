using ReservationService.Model;

namespace ReservationService.Core
{
    public interface IAccommodationRepository : IBaseRepository<Accommodation>
    {
        IEnumerable<Accommodation> SearchByGuestsNum(int guestsNum);
        IEnumerable<Accommodation> SearchByLocation(long location);
        
        IEnumerable<Accommodation> Search(int guestsNum, long location);


    }
}
