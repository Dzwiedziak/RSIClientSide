using ImageService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RSIClientSide.API
{
    [Route("api/images")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService imageService;
        public ImageController(IImageService imageService) {
            this.imageService = imageService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetImage(int id)
        {
            var requestBody = new getImageRequestBody(id);
            var request = new getImageRequest(requestBody);
            var response = await imageService.getImageAsync(request);
            var data = response.Body.@return;

            return File(data, "image/png"); 
        }
    }
}
