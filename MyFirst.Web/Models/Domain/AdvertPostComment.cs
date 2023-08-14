namespace MyFirst.Web.Models.Domain
{
    public class AdvertPostComment
    {
        public Guid Id { get; set; }

        public Guid AdvertPostId { get; set; }
        public Guid UserId { get; set; }

        public DateTime DateAdded { get; set; }

        public string Description { get; set; }
    }
}
