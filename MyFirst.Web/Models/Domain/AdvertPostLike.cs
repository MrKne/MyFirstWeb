namespace MyFirst.Web.Models.Domain
{
    public class AdvertPostLike
    {
        public Guid Id { get; set; }

        public Guid AdvertPostId { get; set; }

        public Guid UserId { get; set; }
    }
}
