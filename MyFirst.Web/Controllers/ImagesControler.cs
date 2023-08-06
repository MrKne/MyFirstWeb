using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFirst.Web.Repositories;
using System.Net;

namespace MyFirst.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesControler : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesControler(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }


        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
           var imageURL = await imageRepository.UploadAsync(file);

            if (imageURL == null)
            {
                return Problem("Something went wrong!",null,(int)HttpStatusCode.InternalServerError);
            }
            return new JsonResult(new { link = imageURL });
        }
    }
}
