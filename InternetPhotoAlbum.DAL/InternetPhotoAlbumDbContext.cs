﻿using InternetPhotoAlbum.DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetPhotoAlbum.DAL
{
    public class InternetPhotoAlbumDbContext : IdentityDbContext<ApplicationUser>
    {
        public InternetPhotoAlbumDbContext(string conectionString) : base(conectionString) { }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserProfile>()
                .HasRequired(x => x.ApplicationUser)
                .WithRequiredDependent(x => x.UserProfile);

            modelBuilder.Entity<Album>()
                .HasRequired(x => x.User)
                .WithMany(x => x.Albums)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<Album>()
                .HasMany(x => x.Images)
                .WithRequired(x => x.Album)
                .HasForeignKey(x => x.AlbumId);

            modelBuilder.Entity<Rating>()
                .HasRequired(r => r.Image)
                .WithMany(i => i.Ratings)
                .HasForeignKey(r => r.ImageId);

            modelBuilder.Entity<Rating>()
                .HasRequired(r => r.User)
                .WithMany(u => u.Ratings)
                .HasForeignKey(r => r.UserId);
        }
    }
}
