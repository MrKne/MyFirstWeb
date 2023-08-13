namespace MyFirst.Web.Models.ViewModels
{
    public class AddLikeRequest
    {
        public Guid AdvertPostId { get; set; }
            public Guid UserId { get; set; }
    }
}
