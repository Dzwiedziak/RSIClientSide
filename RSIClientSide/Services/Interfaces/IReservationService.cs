using RSIClientSide.DTOs;
using RSIClientSide.Models;
using RSIClientSide.Repositories.Interfaces;

namespace RSIClientSide.Services.Interfaces
{
    public interface IReservationService
    {
        public int ReserveVehicle(AddReservationDTO reservation);
        public void UpdateReservation(UpdateReservationDTO reservation);
        public List<CarReservation> GetAll();
    }
}
