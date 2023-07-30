using Microsoft.EntityFrameworkCore;
using MyFirst.Web.Data;
using MyFirst.Web.Models.Domain;

namespace MyFirst.Web.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly MyFirstWebDbContext myFirstWebDbContext;

        public TagRepository(MyFirstWebDbContext myFirstWebDbContext)
        {
            this.myFirstWebDbContext = myFirstWebDbContext;
        }
        public async Task<Tag> AddAsync(Tag tag)
        {

            await myFirstWebDbContext.Tags.AddAsync(tag);
            await myFirstWebDbContext.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var existingTag = await myFirstWebDbContext.Tags.FindAsync(id);
            if (existingTag != null)
            {
                myFirstWebDbContext.Tags.Remove(existingTag);

                await myFirstWebDbContext.SaveChangesAsync();

                return existingTag;
            }
            return null;

        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
          return await myFirstWebDbContext.Tags.ToListAsync();
        }

        public Task<Tag?> GetAsync(Guid id)
        {
           return myFirstWebDbContext.Tags.FirstOrDefaultAsync(x=> x.Id == id);
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag = await myFirstWebDbContext.Tags.FindAsync(tag.Id);
            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName=tag.DisplayName;

                await myFirstWebDbContext.SaveChangesAsync();

                return existingTag;
            }
            return null;
        }
    }
}
