using CarService;
using Microsoft.AspNetCore.Mvc;
using RSIClientSide.DTOs;
using RSIClientSide.Handler;
using RSIClientSide.Models;
using RSIClientSide.Services.Interfaces;
using System.Globalization;

namespace RSIClientSide.API
{
    [Route("api/cars")]
    [ApiController]
    public class CarsForReservationController : ControllerBase
    {
        private ICarForReservationService carForReservationService;
        private IReservationService reservationService;
        private ICarCatalogService carCatalogService;

        public CarsForReservationController(ICarForReservationService carForReservationService,
            IReservationService reservationService,
            ICarCatalogService carCatalogService)
        {
            this.carForReservationService = carForReservationService;
            this.reservationService = reservationService;
            this.carCatalogService = carCatalogService;

            ((CarCatalogServiceClient) carCatalogService).Endpoint.EndpointBehaviors.Add(new MacAddressBehavior());
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            getAllCarsRequest request = new getAllCarsRequest();
            var result = await carCatalogService.getAllCarsAsync(request);
            return Ok(result.@return);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            getCarRequest request = new getCarRequest(id);
            var result = await carCatalogService.getCarAsync(request);
            var data = result.@return;
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost("{carId}/reservations")]
        public async Task<IActionResult> Reserve(int carId, [FromBody] addReservationDTO reservation)
        {
            reservation.carId = carId;
            reservation.period.dateTimeFrom = DateTime.Parse(reservation.period.dateTimeFrom, null, DateTimeStyles.AdjustToUniversal)
                                     .ToLocalTime()
                                     .ToString("yyyy-MM-ddTHH:mm:ss");

            reservation.period.dateTimeTo = DateTime.Parse(reservation.period.dateTimeTo, null, DateTimeStyles.AdjustToUniversal)
                                               .ToLocalTime()
                                               .ToString("yyyy-MM-ddTHH:mm:ss");

            reserveRequest request = new reserveRequest(reservation);
            var result = await carCatalogService.reserveAsync(request);
            return Ok(result.@return);
        }
    }
}
