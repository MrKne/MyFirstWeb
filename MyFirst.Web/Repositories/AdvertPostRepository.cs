using Microsoft.EntityFrameworkCore;
using MyFirst.Web.Data;
using MyFirst.Web.Models.Domain;

namespace MyFirst.Web.Repositories
{
    public class AdvertPostRepository : IAdvertPostRepository
    {
        private readonly MyFirstWebDbContext myFirstWebDbContext;

        public AdvertPostRepository(MyFirstWebDbContext myFirstWebDbContext)
        {
            this.myFirstWebDbContext = myFirstWebDbContext;
        }
        public async Task<AdvertPost> AddAsync(AdvertPost advertPost)
        {
         await myFirstWebDbContext.AddAsync(advertPost);
            await myFirstWebDbContext.SaveChangesAsync();
            return advertPost;
        }

        public Task<AdvertPost?> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AdvertPost>> GetAllAsync()
        {
            return await myFirstWebDbContext.AdvertPosts.Include(x => x.Tags).ToListAsync();
           
        }

        public Task<AdvertPost?> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<AdvertPost?> UpdateAsync(AdvertPost advertPost)
        {
            throw new NotImplementedException();
        }
    }
}
