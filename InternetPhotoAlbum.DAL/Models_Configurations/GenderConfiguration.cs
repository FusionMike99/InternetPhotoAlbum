using InternetPhotoAlbum.DAL.Entities;
using System.Data.Entity.ModelConfiguration;

namespace InternetPhotoAlbum.DAL.Models_Configurations
{
    /// <summary>
    /// Configuration for table Genders
    /// </summary>
    internal class GenderConfiguration : EntityTypeConfiguration<Gender>
    {
        /// <summary>
        /// Set up configuration
        /// </summary>
        public GenderConfiguration()
        {
            HasKey(x => x.Id);

            Property(x => x.Name)
                .HasMaxLength(20)
                .IsRequired();
        }
    }
}
