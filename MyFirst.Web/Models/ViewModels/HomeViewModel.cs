using MyFirst.Web.Models.Domain;

namespace MyFirst.Web.Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<AdvertPost> AdvertPosts { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
    }
}
