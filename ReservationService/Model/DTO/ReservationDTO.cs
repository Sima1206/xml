namespace ReservationService.Model.DTO
{
    public class ReservationDTO
    {
        public Accommodation Accommodation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumGuests { get; set; }
    }
}
