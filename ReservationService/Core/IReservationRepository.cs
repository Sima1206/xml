using ReservationService.Model;

namespace ReservationService.Core
{
    public interface IReservationRepository : IBaseRepository<Reservation>
    {
        IEnumerable<Reservation> SearchByDate(DateTime startDate, DateTime endDate);
        
        IEnumerable<Reservation> GetAllReservationsForAccommodation(long accommodationID, DateTime today);

    }
}
