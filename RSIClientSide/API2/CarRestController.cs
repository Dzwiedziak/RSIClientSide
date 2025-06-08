using CarService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApp.Client;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;

namespace RSIClientSide.API2
{
    [Route("api/cars")]
    [ApiController]
    public class CarRestController : ControllerBase
    {
        ApiClient apiClient { get; init; }
        public CarRestController() {
            var cert = X509CertificateLoader.LoadCertificateFromFile("C:/Users/niedz/Desktop/keys/certificate.crt");
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ClientCertificates.Add(cert);

            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.BaseAddress = new Uri("https://jakob.pl:8843");

            var byteArray = System.Text.Encoding.ASCII.GetBytes("admin:admin");
            httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            apiClient = new ApiClient(httpClient);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await apiClient.GetAllCarsAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            return Ok(await apiClient.GetCarAsync(id));
        }

        [HttpPost("{carId}/reservations")]
        public async Task<IActionResult> Reserve(int carId, [FromBody] addReservationDTO reservation)
        {
            AddReservationDTO addReservationDTO = new AddReservationDTO();
            addReservationDTO.CarId = carId;
            Period period = new Period();
            period.DateTimeFrom = DateTimeOffset.Parse(reservation.period.dateTimeFrom);
            period.DateTimeTo = DateTimeOffset.Parse(reservation.period.dateTimeTo);
            addReservationDTO.Period = period;
            int id = await apiClient.ReserveAsync(addReservationDTO);
            return Ok(id);
        }
    }
}
