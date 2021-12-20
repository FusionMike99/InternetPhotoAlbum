﻿using InternetPhotoAlbum.DAL.Entities;
using System.Data.Entity.ModelConfiguration;

namespace InternetPhotoAlbum.DAL.Models_Configurations
{
    internal class UserProfileConfiguration : EntityTypeConfiguration<UserProfile>
    {
        public UserProfileConfiguration()
        {
            HasKey(x => x.UserId);

            Property(x => x.Name)
                .HasMaxLength(20)
                .IsRequired();

            Property(x => x.Surname)
                .HasMaxLength(20)
                .IsRequired();

            Property(x => x.DateOfBirth)
                .HasColumnType("date")
                .IsOptional();

            HasRequired(x => x.ApplicationUser)
                .WithRequiredDependent(x => x.UserProfile);

            HasRequired(x => x.Gender)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.GenderId);
        }
    }
}
