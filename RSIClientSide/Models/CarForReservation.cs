namespace RSIClientSide.Models
{
    public class CarForReservation
    {
        public int Id { get; set; }
        public string ImgSrc { get; set; }
        public string Model { get; set; }
        public int NumberOfSeats { get; set; }
        public string Description { get; set; }
        public List<int> ReservationIds { get; set; }
    }
}
