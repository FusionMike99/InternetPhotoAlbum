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
            var likeType2 = new LikeType { Id = 2, Name = "Unlike" };

            context.Genders.AddOrUpdate(x => x.Id, gender1, gender2);
            context.LikeTypes.AddOrUpdate(x => x.Id, likeType1, likeType2);

            context.SaveChanges();

            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(context));

            List<string> roles = new List<string> { "user", "admin" };
            foreach (string roleName in roles)
            {
                if (roleManager.FindByName(roleName) == null)
                {
                    var role = new ApplicationRole { Name = roleName };
                    roleManager.Create(role);
                }
            }

            string password = "pa$$w0rd";

            List<ApplicationUser> users = new List<ApplicationUser>
            {
                new ApplicationUser { Id = "75b8e424-b022-47f4-9c40-f827766ec610",
                    UserName = "testUser", Email = "testUser@application.com" },
                new ApplicationUser { Id = "eebbebf7-90d1-4329-9feb-71f537bae1f0",
                    UserName = "admin", Email = "admin@application.com" }
            };

            for (int i = 0; i < users.Count; i++)
            {
                if (userManager.FindByName(users[i].UserName) == null)
                {
                    userManager.Create(users[i], password);
                    userManager.AddToRole(users[i].Id, roles[i]);
                }
            }

            List<UserProfile> userProfiles = new List<UserProfile>
            {
                new UserProfile { UserId = users[0].Id, Name = "Test", Surname = "Testov",
                    GenderId = 1, DateOfBirth = DateTime.Today.AddYears(-24) },
                new UserProfile { UserId = users[1].Id, Name = "Admin", Surname = "Adminov",
                    GenderId = 1, DateOfBirth = DateTime.Today.AddYears(-18) }
            };

            foreach (var userProfile in userProfiles)
            {
                context.UserProfiles.AddOrUpdate(u => u.UserId, userProfile);
            }

            context.SaveChanges();
        }
    }
}
