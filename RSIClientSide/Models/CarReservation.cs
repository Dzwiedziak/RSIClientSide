namespace RSIClientSide.Models
{
    public class CarReservation
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public Period Period { get; set; }
    }
}
