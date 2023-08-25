using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFirst.Web.Models;
using MyFirst.Web.Models.ViewModels;
using MyFirst.Web.Repositories;
using System.Diagnostics;
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
        public async Task<IActionResult> Search(string search, string link)
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

            //var searchResults = advertPosts.Where(advertPosts => advertPosts.Tags.ToString().Contains(search)).ToList();

            //if (searchResults != null)
            //{


            //Console.Write("I found the value");

            //}

            //foreach (var advertPost in advertPosts)

            //{
            //    var separateTag = advertPost.Tags.Equals(search); 

            //    if (separateTag == true) 
            //    {

            //        var searchModel = new SearchViewModel
            //        {
            //            SearchAdvertPosts = advertPosts,
            //            SearchTags = tags,
            //        };


            //        return View(searchModel);

            //    }
            //    else
            //    {
            //        return View(AboutUs);
            //    }


            //}


            //ViewBag.Link = link;
            ViewBag.Link = link;
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