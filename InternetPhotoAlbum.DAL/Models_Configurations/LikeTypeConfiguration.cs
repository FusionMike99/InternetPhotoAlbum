using InternetPhotoAlbum.DAL.Entities;
using System.Data.Entity.ModelConfiguration;

namespace InternetPhotoAlbum.DAL.Models_Configurations
{
    /// <summary>
    /// Configuration for table LikeTypes
    /// </summary>
    internal class LikeTypeConfiguration : EntityTypeConfiguration<LikeType>
    {
        /// <summary>
        /// Set up configuration
        /// </summary>
        public LikeTypeConfiguration()
        {
            HasKey(x => x.Id);

            Property(x => x.Name)
                .HasMaxLength(20)
                .IsRequired();
        }
    }
}
