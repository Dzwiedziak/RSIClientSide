using CarService;
using Microsoft.AspNetCore.Mvc;
using RSIClientSide.DTOs;
using RSIClientSide.Handler;
using RSIClientSide.Models;
using RSIClientSide.Services.Interfaces;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;

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

            var certificate = new X509Certificate2("C:/Users/niedz/Desktop/RSIProject/client.p12", "changeit");

            var client = carCatalogService as CarCatalogServiceClient;
            client.ClientCredentials.ClientCertificate.Certificate = certificate;
            
            client.Endpoint.EndpointBehaviors.Add(new MacAddressBehavior());
            client.ClientCredentials.UserName.UserName = "user";
            client.ClientCredentials.UserName.Password = "user";
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
            reserveRequest request = new reserveRequest(reservation);
            var result = await carCatalogService.reserveAsync(request);
            return Ok(result.@return);
        }
    }
}
