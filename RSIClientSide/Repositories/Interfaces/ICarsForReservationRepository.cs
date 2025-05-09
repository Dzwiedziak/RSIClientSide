using RSIClientSide.Models;

namespace RSIClientSide.Repositories.Interfaces
{
    public interface ICarsForReservationRepository
    {
        List<CarForReservation> GetAll();
        CarForReservation? GetOne(int id);
    }
}
