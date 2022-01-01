using InternetPhotoAlbum.DAL.Entities;
using System.Data.Entity.ModelConfiguration;

namespace InternetPhotoAlbum.DAL.Models_Configurations
{
    internal class RatingConfiguration : EntityTypeConfiguration<Rating>
    {
        public RatingConfiguration()
        {
            HasKey(r => new { r.ImageId, r.UserId });

            HasRequired(r => r.Image)
                .WithMany(i => i.Ratings)
                .HasForeignKey(r => r.ImageId)
                .WillCascadeOnDelete(false);

            HasRequired(r => r.User)
                .WithMany(u => u.Ratings)
                .HasForeignKey(r => r.UserId)
                .WillCascadeOnDelete(false);

            HasRequired(r => r.LikeType)
                .WithMany(lt => lt.Ratings)
                .HasForeignKey(r => r.LikeTypeId)
                .WillCascadeOnDelete(false);
        }
    }
}
