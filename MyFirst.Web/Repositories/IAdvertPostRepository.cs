using MyFirst.Web.Models.Domain;
namespace MyFirst.Web.Repositories
{
    public interface IAdvertPostRepository
    {
        Task<IEnumerable<AdvertPost>> GetAllAsync();
        Task<AdvertPost?> GetByUrlHandleAsync(string urlHandle);
        Task<AdvertPost?> GetAsync(Guid id);
        Task <AdvertPost> AddAsync(AdvertPost advertPost);
        Task<AdvertPost?> UpdateAsync(AdvertPost advertPost);   
        Task <AdvertPost?> DeleteAsync(Guid id);



    }
}
