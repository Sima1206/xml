using Microsoft.AspNetCore.Mvc;
using ReservationService.Core;
using ReservationService.Model.DTO;
using ReservationService.Model;

namespace ReservationService.Services
{
    public class ReservationService : IReservationService
    {
        public Reservation CreateReservation(ReservationDTO? dto)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new ApplicationContext());

                var reservation = new Reservation();
                if (dto != null)
                {
                    reservation.AccommodationId = dto.AccommodationId;
                    reservation.StartDate = dto.StartDate;
                    reservation.EndDate = dto.EndDate;
                    reservation.GuestId = dto.GuestId;
                    reservation.NumGuests = dto.NumGuests;
                    reservation.Accepted = unitOfWork.Accommodations.Get(dto.AccommodationId).AutoAcceptReservations;
                    reservation.TotalPrice = dto.TotalPrice;
                }

                unitOfWork.Reservations.Add(reservation);
                unitOfWork.Complete();
                return reservation;
            }
            catch (Exception e)
            {
                return null!;
            }
        }

        public double TotalPrice(Reservation reservation)
        {
            using UnitOfWork unitOfWork = new(new ApplicationContext());
            var accommodation = unitOfWork.Accommodations.Get(reservation.AccommodationId);
            if (accommodation.PriceForOneGuest)
            {
                return accommodation.Price * (reservation.EndDate - reservation.StartDate).TotalDays * reservation.NumGuests;
            }
            return  accommodation.Price * (reservation.EndDate - reservation.StartDate).TotalDays;
        }

        private void DeleteReservationsWithMatchingPeriod(Reservation reservation)
        {
            using UnitOfWork unitOfWork = new(new ApplicationContext());
            var reservations = unitOfWork.Reservations.GetAll().Where(f => f.Id != reservation.Id);
            foreach (var matchingReservation in reservations)
            {
                if (MatchedAccommodation(matchingReservation, reservation) &&
                    MatchedPeriods(matchingReservation, reservation))
                {
                    DeletePendingReservation(matchingReservation.Id);
                }
            }
        }

        private void DeletePendingReservation(long id)
        {
            using UnitOfWork unitOfWork = new(new ApplicationContext());
            var reservation = unitOfWork.Reservations.Get(id);
            if (reservation.Deleted == false && reservation.Accepted == false)
            {
                reservation.Deleted = true;
                UpdateReservation(reservation);
            }
        }

        public bool CancelReservationByGuest(Reservation reservation)
        {
            using UnitOfWork unitOfWork = new(new ApplicationContext());
            if (!CanItBeCancled(reservation))
            {
                reservation.Deleted = true;
                UpdateReservation(reservation);
                return true;
            }

            return false;
            //servis za usere nek poveca count otkazivanja
        }

        private static bool CanItBeCancled(Reservation reservation)
        {
            var dif = DateTime.Now - reservation.StartDate.Date;
            return dif.TotalDays < 1;
        }

        private static bool MatchedAccommodation(Reservation matchingReservation, Reservation reservation)
        {
            return matchingReservation.AccommodationId == reservation.AccommodationId;
        }

       public object? GetAll()
        {
            using UnitOfWork unitOfWork = new(new ApplicationContext());
            return unitOfWork.Reservations.GetAll();
        }

        public ActionResult<Reservation> GetById(long id)
        {
            using UnitOfWork unitOfWork = new(new ApplicationContext());
            return unitOfWork.Reservations.Get(id);
        }

        private Reservation UpdateReservation(Reservation dto)
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
                return null!;
            }
        }

        public void AcceptReservation(Reservation reservation)
        {
            using UnitOfWork unitOfWork = new(new ApplicationContext());
            reservation.Accepted = true;
            UpdateReservation(reservation);
            DeleteReservationsWithMatchingPeriod(reservation);
        }

        public object? GetByHostId(long id)
        {
            using UnitOfWork unitOfWork = new(new ApplicationContext());

            return (from reservation in unitOfWork.Reservations.GetAll()
                let accommodation = unitOfWork.Accommodations.Get(reservation.AccommodationId)
                where accommodation.HostId == id
                select reservation).ToList();
        }

        public object? GetByAccommodationId(long id)
        {
            using UnitOfWork unitOfWork = new(new ApplicationContext());
            return unitOfWork.Reservations.GetAll().Where(reservation => reservation.AccommodationId == id).ToList();
        }
        private static bool MatchedPeriods(Reservation match, Reservation reservation)
        {
            //poklapaju se ili spada unutar ; poklapaju se ili preklapa ceo period ; pocinje ranije ali se preklapa
            if (match.StartDate >= reservation.StartDate &&
                match.EndDate <= reservation.EndDate ||
                reservation.StartDate >= match.StartDate &&
                reservation.EndDate <= match.EndDate ||
                match.StartDate <= reservation.StartDate &&
                reservation.StartDate <= match.EndDate)
            {
                return true;
            }

            //pocinje kasnije ali se preklapa
            return match.StartDate >= reservation.StartDate &&
                   reservation.StartDate <= match.EndDate;
        }

        public object? GetByGuestId(long id)
        {
            using UnitOfWork unitOfWork = new(new ApplicationContext());
            return unitOfWork.Reservations.GetAll().Where(reservation => reservation.GuestId == id).ToList();

        }

        public object? IsAccommodationAvailable(long id)
        {
            var allAcceptedForAccommodation = AllAcceptedForAccommodation(id);
            var nonAvailableDates = NonAvailableDates(allAcceptedForAccommodation);
            return nonAvailableDates;
        }


        private static List<Reservation> AllAcceptedForAccommodation(long id)
        {
            using UnitOfWork unitOfWork = new(new ApplicationContext());
            return unitOfWork.Reservations.GetAll().Where(reservation => reservation.AccommodationId == id && reservation.Accepted).ToList();
        }

        private static IEnumerable<DateTime> NonAvailableDates(List<Reservation> allAcceptedReservations)
        {
            var nonAvailableDates = new List<DateTime>();
            foreach (var reservation in allAcceptedReservations)
            {
                var dateTime = reservation.StartDate;
                while(dateTime != reservation.EndDate)
                {
                    nonAvailableDates.Add(dateTime);
                    dateTime.AddDays(1);
                }
                nonAvailableDates.Add(reservation.EndDate);
            }
            return nonAvailableDates;
        }

    }
}
