using RSIClientSide.DTOs;
using RSIClientSide.Models;
using RSIClientSide.Repositories.Interfaces;
using RSIClientSide.Services.Interfaces;

namespace RSIClientSide.Services
{
    public class ReservationService : IReservationService
    {
        private readonly ICarReservationRepository _carReservationRepository;

        public ReservationService(ICarReservationRepository carReservationRepository)
        {
            _carReservationRepository = carReservationRepository;
        }

        public int ReserveVehicle(AddReservationDTO reservation)
        {
            if (!IsAvailable(reservation.CarId, reservation.Period)) throw new ArgumentException("Car in this period is reserved");
            return _carReservationRepository.Add(new AddReservationDTO() { CarId = reservation.CarId, Period = reservation.Period });
        }

        public bool IsAvailable(int carId, Period period, int? excludeReservationId = null)
        {
            List<CarReservation> carReservations = _carReservationRepository
                .GetByCarId(carId)
                .Where(reservation => reservation.Id != excludeReservationId)
                .ToList();

            return !carReservations.Any(reservation => HasCommonPeriod(reservation.Period, period));
        }

        public bool HasCommonPeriod(Period fPeriod, Period sPeriod)
        {
            long fStart = fPeriod.DateTimeFrom?.Ticks ?? long.MinValue;
            long fEnd = fPeriod.DateTimeTo?.Ticks ?? long.MaxValue;
            long sStart = sPeriod.DateTimeFrom?.Ticks ?? long.MinValue;
            long sEnd = sPeriod.DateTimeTo?.Ticks ?? long.MaxValue;

            return fStart < sEnd && fEnd > sStart;
        }

        public CarReservation? CreateCarReservation(UpdateReservationDTO updateDTO)
        {
            CarReservation? carReservation = _carReservationRepository.GetOne(updateDTO.Id);
            if (carReservation == null) return null;
            carReservation.Period = updateDTO.Period;
            return carReservation;
        }

        public void UpdateReservation(UpdateReservationDTO reservation)
        {
            CarReservation? modelObj = _carReservationRepository.GetOne(reservation.Id);
            if (modelObj == null) throw new ArgumentException("Reservation does not exist");
            if (!IsAvailable(modelObj.CarId, reservation.Period)) throw new ArgumentException("Car in this period is reserved");
            modelObj.Period = reservation.Period;
            _carReservationRepository.Update(modelObj);
        }

        public List<CarReservation> GetAll()
        {
            return _carReservationRepository.GetAll();
        }
    }
}
