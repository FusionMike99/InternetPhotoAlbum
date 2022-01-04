using InternetPhotoAlbum.DAL.Entities;
using InternetPhotoAlbum.DAL.Models_Configurations;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace InternetPhotoAlbum.DAL
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext() : base("InternetPhotoAlbumDb")
        { }

        public ApplicationContext(string connectionString) : base(connectionString) { }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<LikeType> LikeTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new AlbumConfiguration());
            modelBuilder.Configurations.Add(new GenderConfiguration());
            modelBuilder.Configurations.Add(new ImageConfiguration());
            modelBuilder.Configurations.Add(new LikeTypeConfiguration());
            modelBuilder.Configurations.Add(new RatingConfiguration());
            modelBuilder.Configurations.Add(new UserProfileConfiguration());
        }
    }
}
