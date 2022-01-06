using InternetPhotoAlbum.DAL.Entities;
using InternetPhotoAlbum.DAL.Identity;
using InternetPhotoAlbum.DAL.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Threading.Tasks;

namespace InternetPhotoAlbum.DAL.Repositories
{
    /// <inheritdoc cref="IUnitOfWork"/>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext context;

        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        private AlbumsRepository _albumsRepository;
        private GendersRepository _gendersRepository;
        private ImagesRepository _imagesRepository;
        private LikeTypesRepository _likeTypesRepository;
        private RatingsRepository _ratingsRepository;
        private ProceduresRepository _proceduresRepository;
        private UserProfilesRepository _userProfilesRepository;

        /// <summary>
        /// Inject connection string
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        public UnitOfWork(string connectionString)
        {
            context = new ApplicationContext(connectionString);
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                if (_userManager == null)
                    _userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
                return _userManager;
            }
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                if (_roleManager == null)
                    _roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(context));
                return _roleManager;
            }
        }

        public IAlbumsRepository AlbumsRepository
        {
            get
            {
                if (_albumsRepository == null)
                    _albumsRepository = new AlbumsRepository(context);
                return _albumsRepository;
            }
        }

        public IImagesRepository ImagesRepository
        {
            get
            {
                if (_imagesRepository == null)
                    _imagesRepository = new ImagesRepository(context);
                return _imagesRepository;
            }
        }

        public IRatingsRepository RatingsRepository
        {
            get
            {
                if (_ratingsRepository == null)
                    _ratingsRepository = new RatingsRepository(context);
                return _ratingsRepository;
            }
        }

        public IGendersRepository GendersRepository
        {
            get
            {
                if (_gendersRepository == null)
                    _gendersRepository = new GendersRepository(context);
                return _gendersRepository;
            }
        }

        public ILikeTypesRepository LikeTypesRepository
        {
            get
            {
                if (_likeTypesRepository == null)
                    _likeTypesRepository = new LikeTypesRepository(context);
                return _likeTypesRepository;
            }
        }

        public IUserProfilesRepository UserProfilesRepository
        {
            get
            {
                if (_userProfilesRepository == null)
                    _userProfilesRepository = new UserProfilesRepository(context);
                return _userProfilesRepository;
            }
        }

        public IProceduresRepository ProceduresRepository
        {
            get
            {
                if (_proceduresRepository == null)
                    _proceduresRepository = new ProceduresRepository(context);
                return _proceduresRepository;
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
                    _userManager?.Dispose();
                    _roleManager?.Dispose();
                    _albumsRepository?.Dispose();
                    _gendersRepository?.Dispose();
                    _imagesRepository?.Dispose();
                    _likeTypesRepository?.Dispose();
                    _ratingsRepository?.Dispose();
                    _proceduresRepository?.Dispose();
                    _userProfilesRepository?.Dispose();
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
