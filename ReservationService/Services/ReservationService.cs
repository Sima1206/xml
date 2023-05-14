﻿using System.Collections;
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
                Reservation reservation = new Reservation();

           //     Term term = unitOfWork.Terms.Get(dto.TermId);
            //    Accommodation acc = unitOfWork.Terms.GetTermById(dto.TermId).Accommodation;
                //Accommodation acc = unitOfWork.Accommodations.Get(dto.AccommodationId);

           //     reservation.Term = term;
             
                reservation.StartDate = dto.StartDate;
                reservation.EndDate = dto.EndDate;
                reservation.GuestId = dto.GuestId;
                reservation.NumGuests = dto.NumGuests;
                reservation.Accepted = reservation.Accepted;
                reservation.TotalPrice = TotalPrice(dto);
                reservation.Accepted = false;

          /*      if (!checkDates(dto.StartDate, dto.EndDate, acc.Id, reservation.Id, term.Id))
                   return null;
                   
               
                double price = acc.Price * (dto.EndDate - dto.StartDate).TotalDays;
                
                
                if (acc.PriceForPerson)
                {
                    reservation.TotalPrice = price * reservation.NumGuests + dto.AdditionPrice;
                }
                else
                {
                    reservation.TotalPrice = price + dto.AdditionPrice;
                } */
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
        public void DeletePendingReservation(long id)
        {
            using UnitOfWork unitOfWork = new(new ApplicationContext());
            var reservation= unitOfWork.Reservations.Get(id);
            if (reservation.Deleted == false && reservation.Accepted == false)
            {
                reservation.Deleted = true;
                UpdateReservation(reservation);
            }
        }

        public bool CancelReservationByGuest(Reservation reservation)
        {
            using UnitOfWork unitOfWork = new(new ApplicationContext());
            if (reservation.Accepted == false && !CanItBeCancled(reservation))
            {
                reservation.Deleted = true;
                UpdateReservation(reservation);
                return true;
            }
            return false;
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

        public object? GetByGuestId(long id)
        {     using UnitOfWork unitOfWork = new(new ApplicationContext());
            return unitOfWork.Reservations.GetAll().Where(reservation => reservation.GuestId == id).ToList();

        }

        public object? IsAccommodationAvailable(long id)
        {
            List<Reservation> allAcceptedForAccommodation = AllAcceptedForAccommodation(id);
            IEnumerable<DateTime> nonAvailableDates = NonAvailableDates(allAcceptedForAccommodation);
            return nonAvailableDates;
        }
        private List<Reservation> AllAcceptedForAccommodation(long id)
        {
            using UnitOfWork unitOfWork = new(new ApplicationContext());
            return unitOfWork.Reservations.GetAll().Where(reservation => reservation.AccommodationId == id && reservation.Accepted ).ToList();
        }

        private IEnumerable<DateTime> NonAvailableDates(List<Reservation> allAcceptedReservations)
        {
            var nonAvailableDates = new List<DateTime>();
            foreach (var reservation in allAcceptedReservations)
            {
             nonAvailableDates.Add(reservation.StartDate);
             var dateTime = reservation.StartDate.AddDays(1);
             if (dateTime == reservation.EndDate) break;
            }

            return nonAvailableDates;
}
    /*    public Reservation UpdateReservation(long id, Reservation reservation, long termId)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new ApplicationContext());

                Term term = unitOfWork.Terms.GetTermById(termId);
                
                reservation.Id = id;
                
                if (!checkDates(reservation.StartDate, reservation.EndDate, term.Accommodation.Id, reservation.Id, term.Id))
                  return null;
                
                unitOfWork.Reservations.UpdateReservation(reservation);

            }
            catch(Exception e)
            {
                return null;
            }

            return reservation;
        } */

    /*    private bool checkDates(DateTime StartDate, DateTime EndDate, long AccommodationId, long ReservationId, long TermId)
        {
            using UnitOfWork unitOfWork = new(new ApplicationContext());
            
            if ((EndDate - StartDate).TotalDays <= 0)
            {
                return false;
            }

            var allTerms1 = unitOfWork.Terms.GetAllTerms();
            if (allTerms1.Count > 0)
            {
                var dateIncludes = allTerms1
                    .Any(d => d.Id == TermId && 
                              ((StartDate >= d.StartDate && StartDate < d.EndDate) || (EndDate >= d.StartDate 
                                   && EndDate < d.EndDate) || (d.StartDate >= StartDate  && d.StartDate < EndDate) ||
                               (d.EndDate >= StartDate && d.EndDate < EndDate)));

                if (!dateIncludes)
                    return false;
            }
            
            var allTerms = unitOfWork.Terms.GetAllTerms();
            if (allTerms.Count > 0)
            {
                var dateIncludes = allTerms
                    .Any(d => d.Accommodation.Id == AccommodationId && 
                              ((StartDate >= d.StartDate && StartDate < d.EndDate) || (EndDate >= d.StartDate 
                                   && EndDate < d.EndDate) || (d.StartDate >= StartDate  && d.StartDate < EndDate) ||
                               (d.EndDate >= StartDate && d.EndDate < EndDate)));

                if (!dateIncludes)
                    return false;
            }
            
            var allReservations = unitOfWork.Reservations.GetAllReservations()
                .Where(r => r.Id != ReservationId).ToList();
            if (allReservations.Count > 0)
            {
                var dateIncludes = allReservations
                    .Any(d => d.Term.Accommodation.Id == AccommodationId && 
                              ((StartDate >= d.StartDate && StartDate < d.EndDate) || (EndDate >= d.StartDate 
                                  && EndDate < d.EndDate) || (d.StartDate >= StartDate  && d.StartDate < EndDate) ||
                               (d.EndDate >= StartDate && d.EndDate < EndDate)));

                if (dateIncludes)
                {
                    return false;
                }
            }

            return true;

        } */
    }
}
