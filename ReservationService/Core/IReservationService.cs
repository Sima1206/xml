using Microsoft.AspNetCore.Mvc;
using ReservationService.Model;
using ReservationService.Model.DTO;

namespace ReservationService.Core
{
    public interface IReservationService
    {
        public Reservation CreateReservation(ReservationDTO? dto);

        bool CancelReservationByGuest(Reservation reservation);
        object? GetAll();
        ActionResult<Reservation> GetById(long id);
        void AcceptReservation(Reservation reservation);
        object? GetByGuestId(long id);
        object? IsAccommodationAvailable(long id);
        public double TotalPrice(Reservation dto);
        object? GetByHostId(long id);
        object? GetByAccommodationId(long id);
        
        bool CheckIfAccommodationCanBeReserved(long accommodationID, DateTime startDate, DateTime endDate);

    }
}