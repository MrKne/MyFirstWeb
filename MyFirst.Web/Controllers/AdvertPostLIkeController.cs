using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MyFirst.Web.Models.Domain;
using MyFirst.Web.Models.ViewModels;
using MyFirst.Web.Repositories;

namespace MyFirst.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertPostLikeController : ControllerBase
    {
        private readonly IAdvertPostLikeRepository advertPostLikeRepository;

        public AdvertPostLikeController(IAdvertPostLikeRepository advertPostLikeRepository)
        {
            this.advertPostLikeRepository = advertPostLikeRepository;
        }






        [HttpPost]
        [Route("Add")]

        public async Task<IActionResult> AddLike([FromBody] AddLikeRequest addLikeRequest)
        {
            var model = new AdvertPostLike
            {
                AdvertPostId = addLikeRequest.AdvertPostId,
                UserId = addLikeRequest.UserId,
            };

            await advertPostLikeRepository.AddLikeforAdvert(model);

            return Ok();

        }

        [HttpGet]
        [Route("{advertPostId:Guid}/totalLikes")]
        
        public async Task<IActionResult> GetTotalLikesForAdvert([FromRoute] Guid advertPostId) 
        
        {


            var totalLikes = await advertPostLikeRepository.GetTotalLikes(advertPostId);
            return Ok(totalLikes);

        }

        
    }
}
