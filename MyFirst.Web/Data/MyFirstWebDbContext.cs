using Microsoft.EntityFrameworkCore;
using MyFirst.Web.Models.Domain;
using System.Collections.Generic;

namespace MyFirst.Web.Data
{
    public class MyFirstWebDbContext : DbContext
    {
        public MyFirstWebDbContext(DbContextOptions<MyFirstWebDbContext> options) : base(options)

        {
            //    this.Database.SetCommandTimeout(TimeSpan.FromSeconds(30));
        }


        public DbSet<AdvertPost> AdvertPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public DbSet<AdvertPostLike> AdvertPostsLike { get; set;}

        public DbSet<AdvertPostComment> AdvertPostsComment { get; set; }
    }
}
