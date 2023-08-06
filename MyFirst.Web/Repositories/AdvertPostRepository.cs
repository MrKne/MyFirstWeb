using Microsoft.AspNetCore.Mvc;
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

        public async Task<AdvertPost?> DeleteAsync(Guid id)
        {
            var existingAdvert = await myFirstWebDbContext.AdvertPosts.FindAsync(id);
            if (existingAdvert != null)
            {
                myFirstWebDbContext.AdvertPosts.Remove(existingAdvert);
                await myFirstWebDbContext.SaveChangesAsync();
                return existingAdvert;
            }
            return null;
        }

        public async Task<IEnumerable<AdvertPost>> GetAllAsync()
        {
            return await myFirstWebDbContext.AdvertPosts.Include(x => x.Tags).ToListAsync();
           
        }

        public async Task<AdvertPost?> GetAsync(Guid id)
        {
            return await myFirstWebDbContext.AdvertPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);
        }
        
   
        public async Task<AdvertPost?> GetByUrlHandleAsync(string urlHandle)
        {
         return await myFirstWebDbContext.AdvertPosts.Include(x=> x.Tags)
                .FirstOrDefaultAsync(x=> x.UrlHandle == urlHandle);
        }

        public async Task<AdvertPost?> UpdateAsync(AdvertPost advertPost)
        {
            var existingAdvert = await myFirstWebDbContext.AdvertPosts.Include(x=> x.Tags)
                .FirstOrDefaultAsync(x => x.Id == advertPost.Id);

            if (existingAdvert != null)
            {
                existingAdvert.Id = advertPost.Id;
                existingAdvert.Heading = advertPost.Heading;
                existingAdvert.PageTitle = advertPost.PageTitle;
                existingAdvert.Content = advertPost.Content;
                existingAdvert.ShortDescription = advertPost.ShortDescription;
                existingAdvert.Author = advertPost.Author;
                existingAdvert.FeaturedImageUrl = advertPost.FeaturedImageUrl;
                existingAdvert.UrlHandle = advertPost.UrlHandle;
                existingAdvert.Visible = advertPost.Visible;
                existingAdvert.PublishedDate = advertPost.PublishedDate;
                existingAdvert.Tags = advertPost.Tags;

                await myFirstWebDbContext.SaveChangesAsync();

                return (existingAdvert);

            }
            return null;
        }
    }
}
