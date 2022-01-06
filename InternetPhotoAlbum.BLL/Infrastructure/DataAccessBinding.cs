using InternetPhotoAlbum.DAL.Interfaces;
using InternetPhotoAlbum.DAL.Repositories;
using Ninject.Modules;

namespace InternetPhotoAlbum.BLL.Infrastructure
{
    /// <summary>
    /// Ninject module for data access layer
    /// </summary>
    public class DataAccessBinding : NinjectModule
    {
        private readonly string connectionString;

        /// <summary>
        /// Inject connection string to database
        /// </summary>
        /// <param name="connectionString">Connection string to database</param>
        public DataAccessBinding(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public override void Load()
        {
            Bind<IAlbumsRepository>().To<AlbumsRepository>();
            Bind<IGendersRepository>().To<GendersRepository>();
            Bind<IImagesRepository>().To<ImagesRepository>();
            Bind<ILikeTypesRepository>().To<LikeTypesRepository>();
            Bind<IRatingsRepository>().To<RatingsRepository>();
            Bind<IUserProfilesRepository>().To<UserProfilesRepository>();
            Bind<IUnitOfWork>().To<UnitOfWork>()
                .WithConstructorArgument("connectionString", connectionString);
        }
    }
}
