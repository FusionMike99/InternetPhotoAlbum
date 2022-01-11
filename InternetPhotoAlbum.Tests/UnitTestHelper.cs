using AutoMapper;
using InternetPhotoAlbum.BLL.Infrastructure;

namespace InternetPhotoAlbum.Tests
{
    internal static class UnitTestHelper
    {
        public static Mapper CreateMapperProfile()
        {
            var myProfile = new ModelEntityProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));

            return new Mapper(configuration);
        }

    }
}
