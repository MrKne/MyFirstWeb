using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using MyFirst.Web.Models.ViewModels;
using MyFirst.Web.Repositories;
using Microsoft.AspNetCore.Identity;
using MyFirst.Web.Models.Domain;

namespace MyFirst.Web.Controllers
{
    public class AdvertsController : Controller
    {
        private readonly IAdvertPostRepository advertPostRepository;
        private readonly IAdvertPostLikeRepository advertPostLikeRepository;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IAdvertPostCommentRepository advertPostCommentRepository;

        public AdvertsController(IAdvertPostRepository advertPostRepository,
            IAdvertPostLikeRepository advertPostLikeRepository,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IAdvertPostCommentRepository advertPostCommentRepository)
        
        {
            this.advertPostRepository = advertPostRepository;
            this.advertPostLikeRepository = advertPostLikeRepository;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.advertPostCommentRepository = advertPostCommentRepository;
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
                //get comments for advert 

                var advertCommentsDomainModel = await advertPostCommentRepository.GetCommentsByAdvertIdAsync(advertPost.Id);
                var advertCommentsForView = new List<AdvertComment>();

                foreach (var advertComment in advertCommentsDomainModel)
                {
                    advertCommentsForView.Add(new AdvertComment
                    {
                        DateAdded = advertComment.DateAdded,
                        Description = advertComment.Description,
                        Username = (await userManager.FindByIdAsync(advertComment.UserId.ToString())).UserName
                    });
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
                    Comments = advertCommentsForView

                };

            }
            return View(advertDetailsViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(AdvertDetailsViewModel advertDetailsViewModel)
        {
            if (signInManager.IsSignedIn(User))
            { 
            var domainModel = new AdvertPostComment 
            {
                    AdvertPostId = advertDetailsViewModel.Id,
                    Description = advertDetailsViewModel.CommentDescription,
                    UserId = Guid.Parse(userManager.GetUserId(User)),
                    DateAdded = DateTime.Now
            };
                
                
                await advertPostCommentRepository.AddAsync(domainModel);
                return RedirectToAction("Index", "Adverts", 
                    new {urlHandle = advertDetailsViewModel.UrlHandle});
            }

            return View();
            
        }
    }
}
