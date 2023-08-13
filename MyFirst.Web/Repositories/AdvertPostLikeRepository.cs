using MyFirst.Web.Data;
using Microsoft.EntityFrameworkCore;
using MyFirst.Web.Models.Domain;

namespace MyFirst.Web.Repositories
{ 


    public class AdvertPostLikeRepository : IAdvertPostLikeRepository
{ 
        private readonly MyFirstWebDbContext myFirstWebDbContext;
         public AdvertPostLikeRepository(MyFirstWebDbContext myFirstWebDbContext)
          {
             this.myFirstWebDbContext = myFirstWebDbContext;
          }

        public async Task<AdvertPostLike> AddLikeforAdvert(AdvertPostLike advertPostLike)
        {
            await myFirstWebDbContext.AdvertPostsLike.AddAsync(advertPostLike);
            await myFirstWebDbContext.SaveChangesAsync();
            return advertPostLike;
        }


        public async Task<int> GetTotalLikes(Guid AdvertPostId)
        {
          return await myFirstWebDbContext.AdvertPostsLike
            .CountAsync(x=> x.AdvertPostId == AdvertPostId);
        }
    }
}

