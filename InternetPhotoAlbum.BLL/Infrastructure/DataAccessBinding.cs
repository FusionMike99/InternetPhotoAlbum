using Ninject.Modules;
using InternetPhotoAlbum.DAL.Interfaces;
using InternetPhotoAlbum.DAL.Repositories;

namespace InternetPhotoAlbum.BLL.Infrastructure
{
    public class DataAccessBinding : NinjectModule
    {
        public override void Load()
        {
            Bind<IAlbumsRepository>().To<AlbumsRepository>();
            Bind<IGendersRepository>().To<GendersRepository>();
            Bind<IImagesRepository>().To<ImagesRepository>();
            Bind<ILikeTypesRepository>().To<LikeTypesRepository>();
            Bind<IRatingsRepository>().To<RatingsRepository>();
            Bind<IUserProfilesRepository>().To<UserProfilesRepository>();
            Bind<IUnitOfWork>().To<UnitOfWork>();
        }
    }
}
