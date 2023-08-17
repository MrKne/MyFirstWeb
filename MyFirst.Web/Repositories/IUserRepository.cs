using Microsoft.AspNetCore.Identity;

namespace MyFirst.Web.Repositories
{
    public interface IUserRepository
    {
        public Task<IEnumerable<IdentityUser>>GetAll();
    }
}
