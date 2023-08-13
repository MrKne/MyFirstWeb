using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using MyFirst.Web.Models.ViewModels;
using MyFirst.Web.Repositories;

namespace MyFirst.Web.Controllers
{
    public class AdvertsController : Controller
    {
        private readonly IAdvertPostRepository advertPostRepository;
        private readonly IAdvertPostLikeRepository advertPostLikeRepository;

        public AdvertsController(IAdvertPostRepository advertPostRepository,
            IAdvertPostLikeRepository advertPostLikeRepository)
        {
            this.advertPostRepository = advertPostRepository;
            this.advertPostLikeRepository = advertPostLikeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var advertPost = await advertPostRepository.GetByUrlHandleAsync(urlHandle);
            var advertDetailsViewModel = new AdvertDetailsViewModel();


            if (advertPost != null)
            {
                var totalLikes = await advertPostLikeRepository.GetTotalLikes(advertPost.Id);

                advertDetailsViewModel = new AdvertDetailsViewModel
                {
                    Id = advertPost.Id,
                    Content = advertPost.Content,
                    PageTitle = advertPost.PageTitle,
                    Author = advertPost.Author,
                    FeaturedImageUrl = advertPost.FeaturedImageUrl,
                    Heading = advertPost.Heading,
                    PublishedDate = advertPost.PublishedDate,
                    ShortDescription = advertPost.ShortDescription,
                    UrlHandle = advertPost.UrlHandle,
                    Visible = advertPost.Visible,
                    Tags = advertPost.Tags,
                    TotalLikes = totalLikes

                };

            }
            return View(advertDetailsViewModel);
        }
    }
}
