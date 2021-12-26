using InternetPhotoAlbum.DAL.Entities;
using InternetPhotoAlbum.DAL.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace InternetPhotoAlbum.DAL.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationContext context)
        {
            var gender1 = new Gender { Id = 1, Name = "Male" };
            var gender2 = new Gender { Id = 2, Name = "Female" };

            var likeType1 = new LikeType { Id = 1, Name = "Like" };
            var likeType2 = new LikeType { Id = 2, Name = "Don't like" };

            context.Genders.AddOrUpdate(x => x.Id, gender1, gender2);
            context.LikeTypes.AddOrUpdate(x => x.Id, likeType1, likeType2);

            context.SaveChanges();

            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(context));

            List<string> roles = new List<string> { "user", "admin" };
            foreach (string roleName in roles)
            {
                var role = roleManager.FindByName(roleName);
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    roleManager.Create(role);
                }
            }

            string password = "pa$$w0rd";

            List<ApplicationUser> users = new List<ApplicationUser>
            {
                new ApplicationUser { UserName = "testUser", Email = "testUser@application.com" },
                new ApplicationUser { UserName = "admin", Email = "admin@application.com" }
            };

            for (int i = 0; i < users.Count; i++)
            {
                userManager.Create(users[i], password);
                userManager.AddToRole(users[i].Id, roles[i]);
            }

            List<UserProfile> userProfiles = new List<UserProfile>
            {
                new UserProfile { UserId = users[0].Id, Name = "Test", Surname = "Testov",
                    GenderId = 1, DateOfBirth = DateTime.Today.AddYears(-24) },
                new UserProfile { UserId = users[1].Id, Name = "Admin", Surname = "Adminov",
                    GenderId = 1, DateOfBirth = DateTime.Today.AddYears(-18) }
            };

            context.UserProfiles.AddRange(userProfiles);

            context.SaveChanges();
        }
    }
}
