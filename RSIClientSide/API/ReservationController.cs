using Microsoft.AspNetCore.Mvc;
using RentService;
using RSIClientSide.DTOs;
using RSIClientSide.Services.Interfaces;

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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var request = new deleteReservationRequest(id);
            var result = await carRentalService.deleteReservationAsync(request);
            return Ok();
        }
    }
}
