using RSIClientSide.DTOs;
using RSIClientSide.Models;
using RSIClientSide.Repositories.Interfaces;
using RSIClientSide.Services.Interfaces;

namespace RSIClientSide.Services
{
    public class CarForReservationService : ICarForReservationService
    {
        private ICarsForReservationRepository _repository;
        private ICarReservationRepository _reservationRepository;
        public CarForReservationService(ICarsForReservationRepository repository,
                                        ICarReservationRepository carReservationRepository) { 
            _repository = repository;
            _reservationRepository = carReservationRepository;
        }

        public List<GetCarDTO> GetAll()
        {
            var cars = _repository.GetAll();
            return cars.Select(GetDTO).ToList();
        }

        public GetCarDTO? GetOne(int id)
        {
            var modelObj = _repository.GetOne(id);
            if (modelObj is null)
            {
                return null;
            }
            return GetDTO(modelObj);
        }

        public GetCarDTO GetDTO(CarForReservation car)
        {
            return new GetCarDTO
            {
                Id = car.Id,
                ImgSrc = car.ImgSrc,
                Model = car.Model,
                NumberOfSeats = car.NumberOfSeats,
                Description = car.Description,
                UnavailablePeriods = _reservationRepository
                    .GetByCarId(car.Id)
                    .Select(reservation => reservation.Period)
                    .ToList()
            };
        }
    }
}
