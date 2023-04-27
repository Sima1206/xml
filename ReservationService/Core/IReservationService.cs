using ReservationService.Model;
using ReservationService.Model.DTO;

namespace ReservationService.Core
{
    public interface IReservationService
    {
        public Reservation CreateReservation(ReservationDTO dto);



    }
}
