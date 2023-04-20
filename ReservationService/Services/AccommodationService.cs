using ReservationService.Core;
using ReservationService.Model;
using ReservationService.Model.DTO;

namespace ReservationService.Services
{
    public class AccommodationService : IAccommodationService
    {
        public Accommodation CreateAccommodation(AccommodationDTO dto)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new ApplicationContext());

                Accommodation accommodation = new Accommodation();

                accommodation.Name = dto.Name;
                accommodation.LocationId = dto.LocationId;
                accommodation.Pictures = dto.Pictures;
                accommodation.Kitchen = dto.Kitchen;
                accommodation.Wifi = dto.Wifi;
                accommodation.FreeParking = dto.FreeParking;
                accommodation.MinGuests = dto.MinGuests;
                accommodation.MaxGuests = dto.MaxGuests;
                

                unitOfWork.Accommodations.Add(accommodation);
                unitOfWork.Complete();

                return accommodation;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public IEnumerable<Accommodation> SearchByGuestsNum(int guestsNum)
        {
            try
            {
                using UnitOfWork unitOfWork = new UnitOfWork(new ApplicationContext());

                return unitOfWork.Accommodations.SearchByGuestsNum(guestsNum);
            }
            catch (Exception e)
            {
                return new List<Accommodation>();
            }
        }





    }
}
