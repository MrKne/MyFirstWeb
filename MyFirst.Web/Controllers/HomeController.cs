using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFirst.Web.Models;
using MyFirst.Web.Models.ViewModels;
using MyFirst.Web.Repositories;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace MyFirst.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAdvertPostRepository advertPostRepository;
        private readonly ITagRepository tagRepository;

        public HomeController(ILogger<HomeController> logger, 
            IAdvertPostRepository advertPostRepository,
            ITagRepository tagRepository)
        {
            _logger = logger;
            this.advertPostRepository = advertPostRepository;
            this.tagRepository = tagRepository;
        }

        public async Task<IActionResult> Index()
        {
            //getting all adverts
            var advertPosts = await advertPostRepository.GetAllAsync();

            //get all tags
            var tags = await tagRepository.GetAllAsync();

            var model = new HomeViewModel { 
            AdvertPosts = advertPosts,
            Tags = tags};

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Search(string search)
        {
            //getting all adverts
            var advertPosts = await advertPostRepository.GetAllAsync();

            //get all tags
            var tags = await tagRepository.GetAllAsync();

            var model = new HomeViewModel
            {
                AdvertPosts = advertPosts,
                Tags = tags
            };

            ViewBag.Name = search;

            
          
            return View(model);
        }


        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult ContactUs()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}