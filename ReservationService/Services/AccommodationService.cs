using ReservationService.Configuration;
using ReservationService.Core;
using ReservationService.Model;
using ReservationService.Model.DTO;

namespace ReservationService.Services
{
    public class AccommodationService : IAccommodationService
    {
        
        private readonly IReservationService _reservationService;
        private readonly ProjectConfiguration _projectConfiguration;
        
        public AccommodationService(){}
        public AccommodationService(ProjectConfiguration projectConfiguration, IReservationService reservationService)
        {
            _projectConfiguration = projectConfiguration;
            _reservationService = reservationService;
        }
        public Accommodation CreateAccommodation(AccommodationDTO dto)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new ApplicationContext());

                Accommodation accommodation = new Accommodation();
                accommodation.HostId = dto.HostId;
                accommodation.Name = dto.Name;
                accommodation.LocationId = dto.LocationId;
                accommodation.Pictures = dto.Pictures;
                accommodation.Kitchen = dto.Kitchen;
                accommodation.Wifi = dto.Wifi;
                accommodation.FreeParking = dto.FreeParking;
                accommodation.MinGuests = dto.MinGuests;
                accommodation.MaxGuests = dto.MaxGuests;
                accommodation.Price = dto.Price;
                accommodation.AutoAcceptReservations = dto.AutoAcceptReservations;
                accommodation.PriceForOneGuest = dto.PriceForOneGuest;
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

        public IEnumerable<Accommodation> SearchByLocation(long location)
        {
            try
            {
                using UnitOfWork unitOfWork = new UnitOfWork(new ApplicationContext());

                return unitOfWork.Accommodations.SearchByLocation(location);
            }
            catch (Exception e)
            {
                return new List<Accommodation>();
            }
        }

        public IEnumerable<Reservation> SearchByDate(DateTime startDate, DateTime endDate)
        {
            try
            {
                using UnitOfWork unitOfWork = new UnitOfWork(new ApplicationContext());

                return unitOfWork.Reservations.SearchByDate(startDate, endDate);
            }
            catch (Exception e)
            {
                return new List<Reservation>();
            }
        }
        public bool DeleteAccommodationsByHostId(long hostId)
        {
            using UnitOfWork unitOfWork = new UnitOfWork(new ApplicationContext());
            var accommodations = unitOfWork.Accommodations.GetAll().Where(a => a.HostId == hostId).ToList();
            foreach (var accommodation in accommodations)
            {
                accommodation.Deleted = true;
                //update
            }
            unitOfWork.Complete();
            
            return true;
        }



        public object? GetAll()
        {
            using UnitOfWork unitOfWork = new(new ApplicationContext());
            return unitOfWork.Accommodations.GetAll();
        }

        public object? GetById(long id)
        {
            using UnitOfWork unitOfWork = new(new ApplicationContext());
            return unitOfWork.Accommodations.Get(id);
        }
        public IEnumerable<Accommodation> Search(int guestNum, long location, DateTime startDate, DateTime endDate)
        {
            using UnitOfWork unitOfWork = new UnitOfWork(new ApplicationContext());

            List<Accommodation> found = new List<Accommodation>();
            IEnumerable<Accommodation> accommodations = unitOfWork.Accommodations.Search(guestNum, location);

            foreach (Accommodation acc in accommodations)
            {
                if (_reservationService.CheckIfAccommodationCanBeReserved(acc.Id, startDate, endDate))
                {
                    found.Add(acc);
                }
            }

            return found;
        }
    }
}
