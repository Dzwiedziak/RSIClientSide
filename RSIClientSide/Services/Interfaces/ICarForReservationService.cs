using RSIClientSide.DTOs;
using RSIClientSide.Models;

namespace RSIClientSide.Services.Interfaces
{
    public interface ICarForReservationService
    {
        List<GetCarDTO> GetAll();
        GetCarDTO? GetOne(int id);
    }
}
