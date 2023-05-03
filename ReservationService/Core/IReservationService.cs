using Microsoft.AspNetCore.Mvc;
using ReservationService.Model;
using ReservationService.Model.DTO;

namespace ReservationService.Core
{
    public interface IReservationService
    {
        public Reservation CreateReservation(ReservationDTO dto);


        bool CancelReservationByGuest(Reservation reservation);
        void AutoAcceptReservation(Reservation reservation);
        object? GetAll();
        object? GetAllPending();
        object? GetAllAccepted();
        ActionResult<Reservation> GetById(long id);
        Reservation UpdateReservation(Reservation dto);
      //  bool Delete(int id);
        void AcceptReservation(Reservation reservation);
        object? GetWithMatchingPeriods(long id);
    }
}
