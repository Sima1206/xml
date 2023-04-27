namespace ReservationService.Model.DTO
{
    public class ReservationDTO
    {
        public long AccommodationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumGuests { get; set; }
        public bool? Accepted { get; set; }
        public double TotalPrice { get; set; }


    }
}
