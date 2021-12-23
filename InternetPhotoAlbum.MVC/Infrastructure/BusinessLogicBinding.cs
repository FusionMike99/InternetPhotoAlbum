using InternetPhotoAlbum.BLL.Interfaces;
using InternetPhotoAlbum.BLL.Services;
using Ninject.Modules;

namespace InternetPhotoAlbum.MVC.Infrastructure
{
    public class BusinessLogicBinding : NinjectModule
    {
        public override void Load()
        {
            Bind<IAlbumService>().To<AlbumService>();
            Bind<IGenderService>().To<GenderService>();
            Bind<IImageService>().To<ImageService>();
            Bind<ILikeTypeService>().To<LikeTypeService>();
            Bind<IRatingService>().To<RatingService>();
            Bind<IUserService>().To<UserService>();
        }
    }
}