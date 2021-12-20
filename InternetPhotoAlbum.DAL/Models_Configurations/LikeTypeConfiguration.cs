using InternetPhotoAlbum.DAL.Entities;
using System.Data.Entity.ModelConfiguration;

namespace InternetPhotoAlbum.DAL.Models_Configurations
{
    internal class LikeTypeConfiguration : EntityTypeConfiguration<LikeType>
    {
        public LikeTypeConfiguration()
        {
            HasKey(x => x.Id);

            Property(x => x.Name)
                .HasMaxLength(20)
                .IsRequired();
        }
    }
}
