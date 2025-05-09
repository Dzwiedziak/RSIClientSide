using RSIClientSide.Models;
using RSIClientSide.Repositories.Interfaces;

namespace RSIClientSide.Repositories
{
    public class CarsForReservationHardCodedRepository : ICarsForReservationRepository
    {
        private List<CarForReservation> carsForReservation = new List<CarForReservation>
        {
            new CarForReservation
            {
                Id = 0,
                Description = "Experience the perfect blend of power, style, and technology. This vehicle is built to impress and engineered to perform in any environment.",
                ImgSrc = "https://samochodowy.pl/uploads/blogs/1729238435tnahb-1713735596a494nangjpg.jpeg",
                Model = "Tesla S",
                NumberOfSeats = 5,
                ReservationIds = new List<int> { 0, 1 }
            },
            new CarForReservation
            {
                Id = 1,
                Description = "Powerful vehicle. Rent this car and check it yourself",
                ImgSrc = "https://carleasepolska.pl/media/cache/car_details_thumbnail_small/uploads/5a5346f4bad4e35cec4164b864c6e2df_webp_converted.webp",
                Model = "Audi A6",
                NumberOfSeats = 5,
                ReservationIds = new List<int> { 2 }
            }
        };

        public List<CarForReservation> GetAll()
        {
            return carsForReservation;
        }

        public CarForReservation? GetOne(int id)
        {
            return carsForReservation.Find(x => x.Id == id);    
        }
    }
}
