namespace ReservationService.Model;

public class Term : Entity
{
    public Accommodation Accommodation { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public double AdditionPrice { get; set; }
}