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

                Reservation reservation = new Reservation();

                Accommodation acc = unitOfWork.Accommodations.Get(dto.AccommodationId);

                reservation.Accommodation = acc;
                reservation.StartDate = dto.StartDate;
                reservation.EndDate = dto.EndDate;
                
                reservation.GuestId = dto.GuestId;
                reservation.NumGuests = dto.NumGuests;
                reservation.Accepted = reservation.Accepted;
                reservation.TotalPrice = TotalPrice(acc, dto);

                unitOfWork.Reservations.Add(reservation);
                unitOfWork.Complete();

                return reservation;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private double TotalPrice(Accommodation acc, ReservationDTO reservation)
        {
            var price = acc.Price * (reservation.EndDate - reservation.StartDate).TotalDays;

            return price;
        }

        public void AcceptReservation(Reservation reservation)
        {
            using UnitOfWork unitOfWork = new(new ApplicationContext());

            reservation.Accepted = true;
            unitOfWork.Reservations.Update(reservation);
            DeleteReservationsWithMatchingPeriod(reservation);
        }

        public void AutoAcceptReservation(Reservation reservation)
        {
            using UnitOfWork unitOfWork = new(new ApplicationContext());
            reservation.Accepted = true;
            unitOfWork.Reservations.Update(reservation);
        }
        private void DeleteReservationsWithMatchingPeriod(Reservation reservation)
        {
            using UnitOfWork unitOfWork = new(new ApplicationContext());
            var allReservations = unitOfWork.Reservations.GetAll();
            foreach (var matchingReservation in allReservations)
            {
                if (MatchedAccommodation(matchingReservation, reservation) &&
                    MatchedPeriod(matchingReservation, reservation))
                {

                    matchingReservation.Accepted = false;
                    matchingReservation.Deleted = true;
                    unitOfWork.Reservations.Update(matchingReservation);
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

            if (reservation.Accepted == true && CanItBeCancled(reservation))
            {
                unitOfWork.Reservations.Remove(reservation);
                return true;
            }
            //servis za usere nek poveca count otkazivanja
            return false;
        }

        private bool CanItBeCancled(Reservation reservation)
        {
            TimeSpan dif = DateTime.Now - reservation.StartDate.Date;
            return dif.TotalDays < 2;
        }

        private bool MatchedAccommodation(Reservation matchingReservation, Reservation reservation)
        {
            return matchingReservation.Accommodation == reservation.Accommodation;
        }


        private  bool MatchedPeriod(Reservation matchedReservation, Reservation reservation)
        {
            TimeRange timeRange1 = new TimeRange(matchedReservation.StartDate.Date, matchedReservation.StartDate.Date);
            TimeRange timeRange2 = new TimeRange(reservation.StartDate.Date, reservation.StartDate.Date);
          
            if(timeRange1.IsSamePeriod(timeRange2) &&
            timeRange1.HasInside(timeRange2) &&
            timeRange1.IntersectsWith(timeRange2) &&
            timeRange1.OverlapsWith(timeRange2))
            {
                return true;
            }

            return false;
        }
        
        //na frontu Povuci sve smestaje i pozovi sve rezervacije i filtriraj za svaki smestak datume kad  je slobodan
        public IEnumerable<Reservation> GetAll()
        {
            using UnitOfWork unitOfWork = new(new ApplicationContext());
            return unitOfWork.Reservations.GetAll();
        }
        public Reservation GetById(long id)
        {
            using UnitOfWork unitOfWork = new(new ApplicationContext());
            return unitOfWork.Reservations.Get(id);
        }

        public List<Reservation> GetByHostId(long id)
        {
            using UnitOfWork unitOfWork = new(new ApplicationContext());
            List<Reservation> filteredByHost = new List<Reservation>();
            foreach (Reservation reservation in unitOfWork.Reservations.GetAll())
            {
                if (reservation.Accommodation.HostId == id)
                {
                    filteredByHost.Add(reservation);
                }
            }

            return filteredByHost;
            
        }

        public List<Reservation> GetByAccommodation(long id)
        {
            using UnitOfWork unitOfWork = new(new ApplicationContext());
            List<Reservation> filteredByAccommodationId = new List<Reservation>();
            foreach (Reservation reservation in unitOfWork.Reservations.GetAll())
            {
                if (reservation.Accommodation.Id == id)
                {
                    filteredByAccommodationId.Add(reservation);
                }
            }

            return filteredByAccommodationId;
        }
    }
}
