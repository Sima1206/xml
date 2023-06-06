using ReservationService.Model.DTO;
using ReservationService.Model;

namespace ReservationService.Core
{
    public interface IAccommodationService
    {
        public Accommodation CreateAccommodation(AccommodationDTO dto);
        public IEnumerable<Accommodation> SearchByGuestsNum(int guestsNum);
        public IEnumerable<Accommodation> SearchByLocation(long location);
        public IEnumerable<Reservation> SearchByDate(DateTime startData, DateTime endDate);
        
        IEnumerable<Accommodation> Search(int guestNum, long location, DateTime startDate, DateTime endDate);

    }
}
