namespace ReservationService.Model.DTO
{
    public class AccommodationDTO
    {
        public string Name { get; set; }
        public long LocationId { get; set; }
        public string Pictures { get; set; }
        public bool Wifi { get; set; }
        public bool FreeParking { get; set; }
        public bool Kitchen { get; set; }
        public int MinGuests { get; set; }
        public int MaxGuests { get; set; }
        public double Price { get; set; }
        public bool PriceForPerson { get; set; }

    }
}
