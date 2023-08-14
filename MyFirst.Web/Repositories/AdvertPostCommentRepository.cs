using Microsoft.EntityFrameworkCore;
using MyFirst.Web.Data;
using MyFirst.Web.Models.Domain;

namespace MyFirst.Web.Repositories
{
    public class AdvertPostCommentRepository : IAdvertPostCommentRepository
    {
        private readonly MyFirstWebDbContext myFirstWebDbContext;
        public AdvertPostCommentRepository(MyFirstWebDbContext myFirstWebDbContext)
        {
            this.myFirstWebDbContext = myFirstWebDbContext;
        }
        public async Task<AdvertPostComment> AddAsync(AdvertPostComment advertPostComment)
        {
            await myFirstWebDbContext.AdvertPostsComment.AddAsync(advertPostComment);
            await myFirstWebDbContext.SaveChangesAsync();
            return advertPostComment;
        }

        public async Task<IEnumerable<AdvertPostComment>> GetCommentsByAdvertIdAsync(Guid advertPostId)
        {
            return await myFirstWebDbContext.AdvertPostsComment.Where(x => x.AdvertPostId == advertPostId).ToListAsync();
        }
    }
}
