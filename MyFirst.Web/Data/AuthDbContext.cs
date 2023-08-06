using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MyFirst.Web.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            // Seed Roles (User, Admin, Developer)

            var adminRoleId = "d1ae00d3-acd6-4cb6-ba92-7ce121f96a83";
            var developerRoleId = "3e666c00-bee6-4283-a59a-3fe7d8500fc9";
            var userRoleId = "9d124142-1deb-4fb5-8c3a-fdd45d345d12";

            var roles = new List<IdentityRole>

            {
                  new IdentityRole
                   {
                     Name = "Admin",
                     NormalizedName ="Admin",
                     Id = adminRoleId,
                     ConcurrencyStamp = adminRoleId

                   },

                  new IdentityRole
                   {
                    Name = "Developer",
                    NormalizedName = "Developer",
                    Id = developerRoleId,
                    ConcurrencyStamp = developerRoleId
                   },

                  new IdentityRole
                   {
                   Name = "User",
                   NormalizedName = "User",
                   Id = userRoleId,
                   ConcurrencyStamp = userRoleId
                   },

            };

            builder.Entity<IdentityRole>().HasData(roles);

            //Seed Developer

            var developerId = "9dfa7209-45b7-4f56-abd4-82a6d54efaac";
            var developerUser = new IdentityUser
            {
                UserName = "developer@myfirstweb.com",
                Email = "developer@myfirstweb.com",
                NormalizedEmail = "developer@myfirstweb.com".ToUpper(),
                NormalizedUserName = "developer@myfirstweb.com".ToUpper(),
                Id = developerId
            };

            developerUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(developerUser, "Developer@123");
            builder.Entity<IdentityUser>().HasData(developerUser);

            // Add All roles to Developer
            var developerRoles = new List<IdentityUserRole<string>>
            {

                 new IdentityUserRole<string>
                   {
                        RoleId = adminRoleId,
                        UserId = developerId

                   },

                new IdentityUserRole<string>
                   {
                        RoleId = developerRoleId,
                        UserId = developerId
                   },

                 new IdentityUserRole<string>
                   {
                         RoleId = userRoleId,
                         UserId = developerId
                   }

             };

            builder.Entity<IdentityUserRole<string>>().HasData(developerRoles);

        }
    }
}

      

