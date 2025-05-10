using Microsoft.AspNetCore.Mvc;
using RentService;
using RSIClientSide.DTOs;
using RSIClientSide.Services.Interfaces;
using System.Globalization;

namespace RSIClientSide.API
{
    [ApiController]
    [Route("api/reservations")]
    public class ReservationController : ControllerBase
    {
        IReservationService reservationService;
        ICarRentalService carRentalService;

        public ReservationController(IReservationService reservationService, 
            ICarRentalService carRentalService)
        {
            this.reservationService = reservationService;
            this.carRentalService = carRentalService;
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateReservation(int id, [FromBody] updateReservationDTO reservation)
        {
            if (reservation == null)
                return BadRequest("Reservation data is required.");

            reservation.id = id;
            reservation.period.dateTimeFrom = DateTime.Parse(reservation.period.dateTimeFrom, null, DateTimeStyles.AdjustToUniversal)
                                     .ToLocalTime()
                                     .ToString("yyyy-MM-ddTHH:mm:ss");

            reservation.period.dateTimeTo = DateTime.Parse(reservation.period.dateTimeTo, null, DateTimeStyles.AdjustToUniversal)
                                               .ToLocalTime()
                                               .ToString("yyyy-MM-ddTHH:mm:ss");
            updateReservationRequest request = new updateReservationRequest(reservation);
            carRentalService.updateReservationAsync(request);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            getAllReservationsRequest request = new getAllReservationsRequest();
            var result = await carRentalService.getAllReservationsAsync(request);
            return Ok(result.@return);
        }
    }
}
