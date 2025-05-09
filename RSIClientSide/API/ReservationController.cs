using Microsoft.AspNetCore.Mvc;
using RSIClientSide.DTOs;
using RSIClientSide.Services.Interfaces;

namespace RSIClientSide.API
{
    [ApiController]
    [Route("api/reservations")]
    public class ReservationController : ControllerBase
    {
        IReservationService reservationService;

        public ReservationController(IReservationService reservationService)
        {
            this.reservationService = reservationService;
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateReservation(int id, [FromBody] UpdateReservationDTO reservation)
        {
            if (reservation == null)
                return BadRequest("Reservation data is required.");

            reservation.Id = id;
            try
            {
                reservationService.UpdateReservation(reservation);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Update for this reservation impossible");
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(reservationService.GetAll());
        }
    }
}
