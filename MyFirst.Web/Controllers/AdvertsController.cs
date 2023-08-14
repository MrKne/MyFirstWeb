using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using MyFirst.Web.Models.ViewModels;
using MyFirst.Web.Repositories;
using Microsoft.AspNetCore.Identity;

namespace MyFirst.Web.Controllers
{
    public class AdvertsController : Controller
    {
        private readonly IAdvertPostRepository advertPostRepository;
        private readonly IAdvertPostLikeRepository advertPostLikeRepository;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;

        public AdvertsController(IAdvertPostRepository advertPostRepository,
            IAdvertPostLikeRepository advertPostLikeRepository,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager)
        
        {
            this.advertPostRepository = advertPostRepository;
            this.advertPostLikeRepository = advertPostLikeRepository;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var liked = false;
            var advertPost = await advertPostRepository.GetByUrlHandleAsync(urlHandle);
            var advertDetailsViewModel = new AdvertDetailsViewModel();


            if (advertPost != null)
            {
                var totalLikes = await advertPostLikeRepository.GetTotalLikes(advertPost.Id);

                if (signInManager.IsSignedIn(User))
                {
                    //Get like for this advert for this user
                    var likesForAdvert = await advertPostLikeRepository.GetLikesForAdvert(advertPost.Id);

                    var userId = userManager.GetUserId(User);

                    if (userId != null)
                    {
                       var likeFromUser = likesForAdvert.FirstOrDefault(x => x.UserId == Guid.Parse(userId));
                        liked = likeFromUser != null;
                    }

                }

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
                    TotalLikes = totalLikes,
                    Liked = liked,

                };

            }
            return View(advertDetailsViewModel);
        }
    }
}
