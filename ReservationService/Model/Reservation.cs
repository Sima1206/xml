namespace ReservationService.Model
{
    public class Reservation : Entity
    {
        public long AccommodationId   { get; set; }
   //     public  Term Term { get; set; }
      
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumGuests { get; set; }
        public bool Accepted { get; set; }
        public double TotalPrice { get; set; }

        public long GuestId  { get; set; }

        public Reservation()
        {
        }

        public Reservation(long id, bool deleted, long accommodation,
            DateTime startDate, DateTime endDate, int numGuests, bool accepted, double totalPrice, long guestId)
        {
            id = Id;
            deleted = Deleted;
            accepted = Accepted;
            accommodation = AccommodationId;
            startDate = StartDate;
            endDate = EndDate;
            numGuests = NumGuests;
            totalPrice = TotalPrice;
            guestId = GuestId;
        }

    }
}
