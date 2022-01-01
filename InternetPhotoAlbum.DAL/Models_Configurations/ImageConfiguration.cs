﻿using InternetPhotoAlbum.DAL.Entities;
using System.Data.Entity.ModelConfiguration;

namespace InternetPhotoAlbum.DAL.Models_Configurations
{
    internal class ImageConfiguration : EntityTypeConfiguration<Image>
    {
        public ImageConfiguration()
        {
            HasKey(x => x.Id);

            Property(x => x.Title)
                .HasMaxLength(20)
                .IsRequired();

            Property(x => x.Description)
                .HasMaxLength(200)
                .IsOptional();

            Property(x => x.AddedDate)
                .HasColumnType("datetime")
                .IsRequired();

            Property(x => x.File)
                .IsRequired();

            HasRequired(i => i.Album)
                .WithMany(a => a.Images)
                .HasForeignKey(i => i.AlbumId)
                .WillCascadeOnDelete(false);
        }
    }
}
