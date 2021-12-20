using InternetPhotoAlbum.DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace InternetPhotoAlbum.DAL
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {        
        public ApplicationContext() : base("InternetPhotoAlbumDb")
        { }

        public ApplicationContext(string conectionString) : base(conectionString) { }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<LikeType> LikeTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<UserProfile>()
                .HasRequired(x => x.ApplicationUser)
                .WithRequiredDependent(x => x.UserProfile);

            modelBuilder.Entity<UserProfile>()
                .HasRequired(x => x.Gender)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.GenderId);

            modelBuilder.Entity<Album>()
                .HasRequired(x => x.User)
                .WithMany(x => x.Albums)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<Album>()
                .HasMany(x => x.Images)
                .WithRequired(x => x.Album)
                .HasForeignKey(x => x.AlbumId);

            modelBuilder.Entity<Rating>()
                .HasKey(r => new { r.ImageId, r.UserId });

            modelBuilder.Entity<Rating>()
                .HasRequired(r => r.Image)
                .WithMany(i => i.Ratings)
                .HasForeignKey(r => r.ImageId);

            modelBuilder.Entity<Rating>()
                .HasRequired(r => r.User)
                .WithMany(u => u.Ratings)
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<Rating>()
                .HasRequired(r => r.LikeType)
                .WithMany(lt => lt.Ratings)
                .HasForeignKey(r => r.LikeTypeId);
        }
    }
}
