using Microsoft.EntityFrameworkCore;
using ReservationService.Core;
using ReservationService.Model;
using ReservationService.Model.DTO;

namespace ReservationService.Repository
{
    public class ReservationRepository : BaseRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(DbContext context) : base(context)
        {
        }
        public IEnumerable<Reservation> SearchByDate(DateTime startData, DateTime endDate)
        {
            return ApplicationContext.Reservations.Where(x => startData > x.StartDate && endDate < x.EndDate).ToList();
        }

        public List<Reservation> GetAllReservations() //Izvlaci sve rezervacije i include-uje accomodation u rezervaciji koji je deo nje
        {                                              //kupi objekat od stranog kljuca
            return ApplicationContext.Reservations.Include(r => r.Term.Accommodation).AsNoTracking().ToList();
        }

        public void UpdateReservation(Reservation reservation)
        {
            ApplicationContext.Entry(reservation).State = EntityState.Modified; 
            ApplicationContext.SaveChanges(); 
        }
    }
}
