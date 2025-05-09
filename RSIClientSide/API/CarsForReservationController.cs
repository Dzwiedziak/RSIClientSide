using Microsoft.AspNetCore.Mvc;
using RSIClientSide.DTOs;
using RSIClientSide.Models;
using RSIClientSide.Services.Interfaces;

namespace RSIClientSide.API
{
    [Route("api/cars")]
    [ApiController]
    public class CarsForReservationController : ControllerBase
    {
        private ICarForReservationService carForReservationService;
        private IReservationService reservationService;

        public CarsForReservationController(ICarForReservationService carForReservationService, IReservationService reservationService)
        {
            this.carForReservationService = carForReservationService;
            this.reservationService = reservationService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(carForReservationService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetOne(int id)
        {
            var car = carForReservationService.GetOne(id);
            if (car == null)
            {
                return NotFound();
            }
            return Ok(car);
        }

        [HttpPost("{carId}/reservations")]
        public IActionResult Reserve(int carId, [FromBody] AddReservationDTO reservation)
        {
            reservation.CarId = carId;
            
            try
            {
                int id = reservationService.ReserveVehicle(reservation);
                return Ok(id);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
