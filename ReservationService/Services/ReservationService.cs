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
                
                
              // var numOfDays = (dto.EndDate - dto.StartDate).TotalDays;

               /*     if (numOfDays <= 0)
                   {
                       return null;
                   }
   
                   var allReservations = unitOfWork.Reservations.GetAllReservations();
                   if (allReservations.Count > 0)
                   {
                      var dateIncludes = allReservations
                           .Any(d => d.Accommodation.Id  == dto.AccommodationId && ((dto.StartDate >= d.StartDate && 
                               dto.StartDate < d.EndDate) || (dto.EndDate >= d.StartDate && dto.EndDate < d.EndDate))); 
   
                    
                       if (dateIncludes)
                       {
                           return null;
                       }
                   }   */

               if (!checkDates(dto.StartDate, dto.EndDate, dto.AccommodationId))
                   return null;
                   
               
                double price = acc.Price * (dto.EndDate - dto.StartDate).TotalDays;
                
                
                if (acc.PriceForPerson)
                {
                    reservation.TotalPrice = price * reservation.NumGuests + dto.AdditionPrice;
                }
                else
                {
                    reservation.TotalPrice = price + dto.AdditionPrice;
                }

                unitOfWork.Reservations.Add(reservation);
                unitOfWork.Complete();

                return reservation;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Reservation UpdateReservation(long id, Reservation reservation, long accommodationId)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new ApplicationContext());
                
                reservation.Id = id;
                
                if (!checkDates(reservation.StartDate, reservation.EndDate, accommodationId))
                  return null;
                
                unitOfWork.Reservations.UpdateReservation(reservation);

            }
            catch(Exception e)
            {
                return null;
            }

            return reservation;
        }


        private bool checkDates(DateTime StartDate, DateTime EndDate, long AccommodationId)
        {
            using UnitOfWork unitOfWork = new(new ApplicationContext());
            
            if ((EndDate - StartDate).TotalDays <= 0)
            {
                return false;
            }

            var allReservations = unitOfWork.Reservations.GetAllReservations();
            if (allReservations.Count > 0)
            {
                var dateIncludes = allReservations
                    .Any(d => d.Accommodation.Id == AccommodationId && 
                              ((StartDate >= d.StartDate && StartDate < d.EndDate) || (EndDate >= d.StartDate 
                                  && EndDate < d.EndDate) || (d.StartDate >= StartDate  && d.StartDate < EndDate) ||
                               (d.EndDate >= StartDate && d.EndDate < EndDate)));

                if (dateIncludes)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
