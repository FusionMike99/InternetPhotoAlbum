using AutoMapper;
using InternetPhotoAlbum.BLL.Models;
using InternetPhotoAlbum.DAL.Entities;

namespace InternetPhotoAlbum.BLL.Infrastructure
{
    public class ModelEntityProfile : Profile
    {
        public ModelEntityProfile()
        {
            CreateMap<UserProfile, UserDTO>()
                .ForMember(u => u.Id, x => x.MapFrom(up => up.UserId))
                .ReverseMap();

            CreateMap<UserProfile, EditUserProfileModel>()
                .ForMember(u => u.Id, x => x.MapFrom(up => up.UserId))
                .ReverseMap();

            CreateMap<Album, AlbumDTO>()
                .ReverseMap();
        }
    }
}
