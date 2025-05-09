using RSIClientSide.Models;

namespace RSIClientSide.DTOs
{
    public class AddReservationDTO
    {
        public int CarId { get; set; }
        public Period Period { get; set; }
    }
}
