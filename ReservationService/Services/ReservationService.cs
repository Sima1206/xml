using System.Collections;
using System.Security.AccessControl;
using Itenso.TimePeriod;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

                var reservation = new Reservation();
                reservation.AccommodationId = dto.AccommodationId;
                reservation.StartDate = dto.StartDate;
                reservation.EndDate = dto.EndDate;
                reservation.GuestId = dto.GuestId;
                reservation.NumGuests = dto.NumGuests;
                reservation.Accepted = reservation.Accepted;
                reservation.TotalPrice = TotalPrice(dto);
                unitOfWork.Reservations.Add(reservation);
                unitOfWork.Complete();

                return reservation;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private double TotalPrice(ReservationDTO reservation)
        {
            
            using UnitOfWork unitOfWork = new(new ApplicationContext());
            var accommodation = unitOfWork.Accommodations.Get(reservation.AccommodationId);
            var price = accommodation.Price * (reservation.EndDate - reservation.StartDate).TotalDays;
            return price;
        }
        
        
        public void AutoAcceptReservation(Reservation reservation)
        {
            using UnitOfWork unitOfWork = new(new ApplicationContext());
            reservation.Accepted = true;
            UpdateReservation(reservation);
        }
        
        public void DeleteReservationsWithMatchingPeriod(Reservation reservation)
        {
            using UnitOfWork unitOfWork = new(new ApplicationContext());
            var allReservations = unitOfWork.Reservations.GetAll();
            var allMatchedReservation = allReservations.Where(f => f.Id != reservation.Id);
            foreach (var matchingReservation in allMatchedReservation)
            {
                if (MatchedAccommodation(matchingReservation, reservation) &&
                    MatchedPeriods(matchingReservation, reservation))
                {

                    matchingReservation.Accepted = false;
                    matchingReservation.Deleted = true;
                    UpdateReservation(matchingReservation);
                }
            }
        }

        //mozda na front i da ne prikazujes one sto je host odbio
        public void DeletePendingReservation(long id)
        {
            using UnitOfWork unitOfWork = new(new ApplicationContext());
            var reservation= unitOfWork.Reservations.Get(id);
            if (reservation.Deleted == false && reservation.Accepted == false)
            {
                //nije obrisana od strane hosta kad je prihvatio ponudu tj stavio deleted na true
                unitOfWork.Reservations.Remove(reservation);
            }
        }

        public bool CancelReservationByGuest(Reservation reservation)
        {
            using UnitOfWork unitOfWork = new(new ApplicationContext());
            if (!reservation.Accepted || !CanItBeCancled(reservation)) return false;
            unitOfWork.Reservations.Remove(reservation);
            return true;
            //servis za usere nek poveca count otkazivanja
        }

        private bool CanItBeCancled(Reservation reservation)
        {
            var dif = DateTime.Now - reservation.StartDate.Date;
            return dif.TotalDays < 2;
        }

        private bool MatchedAccommodation(Reservation matchingReservation, Reservation reservation)
        {
            return matchingReservation.AccommodationId == reservation.AccommodationId;
        }
        //na frontu Povuci sve smestaje i pozovi sve rezervacije i filtriraj za svaki smestak datume kad  je slobodan
        public object? GetAll()
        {
            using UnitOfWork unitOfWork = new(new ApplicationContext());
            return unitOfWork.Reservations.GetAll();
        }
        public object? GetAllPending()
        {
            //nisu obrisane a nisu ni prihvacene
            using UnitOfWork unitOfWork = new(new ApplicationContext());
            return unitOfWork.Reservations.GetAll().Where(reservation => reservation.Accepted == false && reservation.Deleted==false).ToList();;;
        }
        
        public object? GetAllAccepted()
        {
            using UnitOfWork unitOfWork = new(new ApplicationContext());
            return unitOfWork.Reservations.GetAll().Where(reservation => reservation.Accepted).ToList();
        }
        public ActionResult<Reservation> GetById(long id)
        {
            using UnitOfWork unitOfWork = new(new ApplicationContext());
            return unitOfWork.Reservations.Get(id);
        }

        public Reservation UpdateReservation(Reservation dto)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new ApplicationContext());

                unitOfWork.Reservations.Update(dto);
                unitOfWork.Complete();

                return dto;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public void AcceptReservation(Reservation reservation)
        {
            using UnitOfWork unitOfWork = new(new ApplicationContext());
            reservation.Accepted = true;
            UpdateReservation(reservation);
            DeleteReservationsWithMatchingPeriod(reservation);
        }

        public List<Reservation> GetByHostId(long id)
        {
            using UnitOfWork unitOfWork = new(new ApplicationContext());

            return (from reservation in unitOfWork.Reservations.GetAll()
                let accommodation = unitOfWork.Accommodations.Get(reservation.AccommodationId)
                where accommodation.HostId == id select reservation).ToList();
        }
        
        public List<Reservation> GetByGuest(long id)
        {
            using UnitOfWork unitOfWork = new(new ApplicationContext());

            return unitOfWork.Reservations.GetAll().Where(reservation => reservation.GuestId == id).ToList();
        }

        public List<Reservation> GetByAccommodation(long id)
        {
            using UnitOfWork unitOfWork = new(new ApplicationContext());
            return unitOfWork.Reservations.GetAll().Where(reservation => reservation.AccommodationId == id).ToList();
        }

        public object? GetWithMatchingPeriods(long id)
        {
            using UnitOfWork unitOfWork = new(new ApplicationContext());
            var allReservations = unitOfWork.Reservations.GetAll();
            var reservation = unitOfWork.Reservations.Get(id);
            List<Reservation> matchedReservations = new List<Reservation>();
            foreach (var matchingReservation in allReservations)
            {
                if (MatchedAccommodation(matchingReservation, reservation) &&
                    MatchedPeriods(matchingReservation, reservation))
                {
                    matchedReservations.Add(matchingReservation);
                }
            }
            return matchedReservations;
        }

        private bool MatchedPeriods(Reservation matchingReservation, Reservation reservation)
        {
            //poklapaju se ili spada unutar 
            if (matchingReservation.StartDate >= reservation.StartDate &&
                matchingReservation.EndDate <= reservation.EndDate)
            {
                return true;
            }
            //poklapaju se ili preklapa ceo period
            if (reservation.StartDate >= matchingReservation.StartDate &&
                reservation.EndDate <= matchingReservation.EndDate)
            {
                return true;
            }
            //pocinje ranije ali se preklapa
            if (matchingReservation.StartDate <= reservation.StartDate &&
                reservation.StartDate <= matchingReservation.EndDate)
            {
                return true;
            }
            //pocinje kasnije ali se preklapa
            if (matchingReservation.StartDate >= reservation.StartDate &&
                reservation.StartDate <= matchingReservation.EndDate)
            {
                return true;
            }
            return false;
        }
    }
}
