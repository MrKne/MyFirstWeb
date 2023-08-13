using MyFirst.Web.Models.Domain;

namespace MyFirst.Web.Repositories
{
    public interface IAdvertPostLikeRepository
    {
        Task<int> GetTotalLikes(Guid AdvertPostId);

        Task<AdvertPostLike> AddLikeforAdvert(AdvertPostLike advertPostLike);

    }
}
