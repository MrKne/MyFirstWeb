using MyFirst.Web.Models.Domain; 
namespace MyFirst.Web.Repositories
{ 
    public interface IAdvertPostCommentRepository
    {
    Task<AdvertPostComment> AddAsync(AdvertPostComment advertPostComment);

        Task<IEnumerable<AdvertPostComment>> GetCommentsByAdvertIdAsync(Guid advertPostId);

    }
}
