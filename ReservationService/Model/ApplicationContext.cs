using Microsoft.EntityFrameworkCore;

namespace ReservationService.Model
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Accommodation> Accommodations { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}
