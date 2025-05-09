using RSIClientSide.Models;

namespace RSIClientSide.DTOs
{
    public class GetCarDTO
    {
        public int Id { get; set; }
        public string ImgSrc { get; set; }
        public string Model { get; set; }
        public int NumberOfSeats { get; set; }
        public string Description { get; set; }
        public List<Period> UnavailablePeriods { get; set; }
    }
}
