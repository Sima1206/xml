using ReservationService.Model.DTO;
using ReservationService.Model;

namespace ReservationService.Core
{
    public interface IAccommodationService
    {
        public Accommodation CreateAccommodation(AccommodationDTO dto);
    }
}
