using ReservationService.Model;

namespace ReservationService.Core
{
    public interface IAccommodationRepository : IBaseRepository<Accommodation>
    {
        IEnumerable<Accommodation> SearchByGuestsNum(int guestsNum);
    }
}
