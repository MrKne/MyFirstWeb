using Microsoft.EntityFrameworkCore;
using MyFirst.Web.Data;
using MyFirst.Web.Models.Domain;
using System.Security.Cryptography.X509Certificates;

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

        public async Task<AdvertPost?> GetAsync(Guid id)
        {
            return await myFirstWebDbContext.AdvertPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<AdvertPost?> UpdateAsync(AdvertPost advertPost)
        {
            throw new NotImplementedException();
        }
    }
}
