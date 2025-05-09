using RSIClientSide.DTOs;
using RSIClientSide.Models;
using RSIClientSide.Repositories.Interfaces;

namespace RSIClientSide.Repositories
{
    public class CarReservationHardCodedRepository : ICarReservationRepository
    {
        List<CarReservation> carReservations = new List<CarReservation>()
        {
            new CarReservation
            {
                Id = 0,
                CarId = 0,
                Period = new Period { DateTimeFrom = null, DateTimeTo = DateTime.Now }
            },
            new CarReservation
            {
                Id = 1,
                CarId = 0,
                Period = new Period { DateTimeFrom = new DateTime(2025, 4, 7), DateTimeTo = new DateTime(2025, 4, 9) }
            },
            new CarReservation
            {
                Id = 2,
                CarId = 1,
                Period = new Period { DateTimeFrom = null, DateTimeTo = DateTime.Now }
            }
        };

        private int currentId = 3;

        public int Add(AddReservationDTO reservation)
        {
            var modelObj = CreateCarReservation(reservation);
            carReservations.Add(modelObj);
            return currentId++;
        }

        public List<CarReservation> GetAll()
        {
            return carReservations;
        }

        public List<CarReservation> GetByCarId(int id)
        {
            return carReservations.FindAll(x => x.CarId == id);
        }

        public CarReservation? GetOne(int id)
        {
            return carReservations.Find(x => x.CarId == id);
        }

        public void Update(CarReservation carReservation)
        {
            int index = carReservations.FindIndex(reservation => reservation.CarId == carReservation.CarId);

            if (index != -1)
            {
                carReservations[index] = carReservation;
            }
        }

        public CarReservation CreateCarReservation(AddReservationDTO addReservation)
        {
            return new()
            {
                Id = currentId,
                CarId = addReservation.CarId,
                Period = addReservation.Period
            };
        }
    }
}
