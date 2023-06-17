namespace ReservationService.Model;

public class SpecialPrice : Entity
{
    public long AccommodationId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public double NewPrice { get; set; }
}
