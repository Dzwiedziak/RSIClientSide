using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApp.Client;
using RentService;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;

namespace RSIClientSide.API2
{
    [Route("api/reservations")]
    [ApiController]
    public class ReservationRestController : ControllerBase
    {
        ApiClient apiClient { get; init; }
        public ReservationRestController() {
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

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateReservation(int id, [FromBody] updateReservationDTO reservation)
        {
            UpdateReservationDTO reservationDTO = new UpdateReservationDTO();
            reservationDTO.Id = id;
            Period period = new Period();
            period.DateTimeFrom = DateTimeOffset.Parse(reservation.period.dateTimeFrom);
            period.DateTimeTo = DateTimeOffset.Parse(reservation.period.dateTimeTo);
            reservationDTO.Period = period;
            await apiClient.UpdateReservationAsync(reservationDTO);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await apiClient.GetAllReservationsAsync());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            await apiClient.DeleteReservationAsync(id);
            return Ok();
        }
    }
}
