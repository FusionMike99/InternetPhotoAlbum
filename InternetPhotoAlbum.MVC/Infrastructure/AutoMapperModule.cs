using AutoMapper;
using InternetPhotoAlbum.BLL.Infrastructure;
using Ninject;
using Ninject.Modules;

namespace InternetPhotoAlbum.MVC.Infrastructure
{
    /// <summary>
    /// Ninject module for automapper
    /// </summary>
    public class AutoMapperModule : NinjectModule
    {
        public override void Load()
        {
            var mapperConfiguration = CreateConfiguration();
            Bind<MapperConfiguration>().ToConstant(mapperConfiguration).InSingletonScope();

            Bind<IMapper>().ToMethod(ctx =>
                 new Mapper(mapperConfiguration, type => ctx.Kernel.Get(type)));
        }

        private MapperConfiguration CreateConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(ModelEntityProfile), typeof(ViewModelModelProfile));
            });

            return config;
        }
    }
}