using Microsoft.AspNetCore.Mvc;
using MyFirst.Web.Repositories;

namespace MyFirst.Web.Controllers
{
    public class AdvertsController : Controller
    {
        private readonly IAdvertPostRepository advertPostRepository;

        public AdvertsController(IAdvertPostRepository advertPostRepository)
        {
            this.advertPostRepository = advertPostRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var advertPost = await advertPostRepository.GetByUrlHandleAsync(urlHandle);
            return View(advertPost);
        }
    }
}
