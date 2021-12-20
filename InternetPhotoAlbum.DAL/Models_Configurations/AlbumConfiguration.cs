using InternetPhotoAlbum.DAL.Entities;
using System.Data.Entity.ModelConfiguration;

namespace InternetPhotoAlbum.DAL.Models_Configurations
{
    internal class AlbumConfiguration : EntityTypeConfiguration<Album>
    {
        public AlbumConfiguration()
        {
            HasKey(x => x.Id);

            Property(x => x.Title)
                .HasMaxLength(20)
                .IsRequired();

            Property(x => x.Description)
                .HasMaxLength(200)
                .IsOptional();

            Property(x => x.PeriodStart)
                .HasColumnType("Date")
                .IsRequired();

            Property(x => x.PeriodEnd)
                .HasColumnType("Date")
                .IsOptional();

            HasRequired(x => x.User)
                .WithMany(x => x.Albums)
                .HasForeignKey(x => x.UserId);
        }
    }
}
