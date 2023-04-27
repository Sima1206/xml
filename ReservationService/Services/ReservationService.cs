using ReservationService.Core;
using ReservationService.Model.DTO;
using ReservationService.Model;

namespace ReservationService.Services
{
    public class ReservationService : IReservationService
    {
        public Reservation CreateReservation(ReservationDTO dto)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new ApplicationContext());

                Reservation reservation = new Reservation();

                Accommodation acc = unitOfWork.Accommodations.Get(dto.AccommodationId);

                reservation.Accommodation = acc;
                reservation.StartDate = dto.StartDate;
                reservation.EndDate = dto.EndDate;
                reservation.NumGuests = dto.NumGuests;
                reservation.Accepted = false;
                reservation.TotalPrice = acc.Price * (dto.EndDate - dto.StartDate).TotalDays;


                unitOfWork.Reservations.Add(reservation);
                unitOfWork.Complete();

                return reservation;
            }
            catch (Exception e)
            {
                return null;
            }
        }





    }
}
