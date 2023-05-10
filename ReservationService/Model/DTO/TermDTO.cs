namespace ReservationService.Model.DTO;

public class TermDTO
{
    public long AccommodationId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public  double AdditionPrice {get; set; }
}