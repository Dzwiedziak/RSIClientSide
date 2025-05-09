using RSIClientSide.DTOs;
using RSIClientSide.Models;

namespace RSIClientSide.Repositories.Interfaces
{
    public interface ICarReservationRepository
    {
        List<CarReservation> GetAll();
        CarReservation? GetOne(int id);
        List<CarReservation> GetByCarId(int id);
        int Add(AddReservationDTO reservation);
        void Update(CarReservation carReservation);
    }
}
