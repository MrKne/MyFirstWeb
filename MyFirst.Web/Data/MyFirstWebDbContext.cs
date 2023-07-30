using Microsoft.EntityFrameworkCore;
using MyFirst.Web.Models.Domain;
using System.Collections.Generic;

namespace MyFirst.Web.Data
{
    public class MyFirstWebDbContext : DbContext
    {
        public MyFirstWebDbContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<AdvertPost> AdvertPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
