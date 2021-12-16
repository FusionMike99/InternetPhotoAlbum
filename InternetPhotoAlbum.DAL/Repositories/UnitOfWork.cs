using InternetPhotoAlbum.DAL.Entities;
using InternetPhotoAlbum.DAL.Identity;
using InternetPhotoAlbum.DAL.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetPhotoAlbum.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly InternetPhotoAlbumDbContext context;

        private ApplicationUserManager userManager;
        private ApplicationRoleManager roleManager;
        private AlbumsRepository albumsRepository;
        private ImagesRepository imagesRepository;
        private RatingsRepository ratingsRepository;

        public UnitOfWork(string connectionString)
        {
            context = new InternetPhotoAlbumDbContext(connectionString);
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                if (userManager == null)
                    userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
                return userManager;
            }
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                if (roleManager == null)
                    roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(context));
                return roleManager;
            }
        }

        public IAlbumsRepository AlbumsRepository
        {
            get
            {
                if (albumsRepository == null)
                    albumsRepository = new AlbumsRepository(context);
                return albumsRepository;
            }
        }

        public IImagesRepository ImagesRepository
        {
            get
            {
                if (imagesRepository == null)
                    imagesRepository = new ImagesRepository(context);
                return imagesRepository;
            }
        }

        public IRatingsRepository RatingsRepository
        {
            get
            {
                if (ratingsRepository == null)
                    ratingsRepository = new RatingsRepository(context);
                return ratingsRepository;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    userManager.Dispose();
                    roleManager.Dispose();
                    albumsRepository.Dispose();
                    imagesRepository.Dispose();
                    ratingsRepository.Dispose();
                }
                disposed = true;
            }
        }

        public async Task<int> SaveAsync()
        {
            var result = await context.SaveChangesAsync();
            return result;
        }
    }
}
