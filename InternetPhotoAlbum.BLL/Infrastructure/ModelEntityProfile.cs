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
                .ForMember(u => u.GenderName, x => x.MapFrom(up => up.Gender.Name));

            CreateMap<UserDTO, UserProfile>()
                .ForMember(u => u.UserId, x => x.MapFrom(up => up.Id));

            CreateMap<ApplicationUser, UserDTO>()
                .ForMember(u => u.Name, x => x.MapFrom(up => up.UserProfile.Name))
                .ForMember(u => u.Login, x => x.MapFrom(up => up.UserName))
                .ForMember(u => u.Surname, x => x.MapFrom(up => up.UserProfile.Surname))
                .ForMember(u => u.DateOfBirth, x => x.MapFrom(up => up.UserProfile.DateOfBirth))
                .ForMember(u => u.IsLocked, x => x.MapFrom(up => up.LockoutEnabled))
                .ForMember(u => u.GenderId, x => x.MapFrom(up => up.UserProfile.GenderId))
                .ForMember(u => u.GenderName, x => x.MapFrom(up => up.UserProfile.Gender.Name));

            CreateMap<UserDTO, EditUserProfileModel>();

            CreateMap<EditUserProfileModel, UserProfile>()
                .ForMember(u => u.UserId, x => x.MapFrom(u => u.Id));

            CreateMap<Album, AlbumDTO>()
                .ForMember(a => a.UserName, x => x.MapFrom(a => a.User.UserName))
                .ReverseMap();

            CreateMap<Gender, GenderDTO>()
                .ReverseMap();

            CreateMap<LikeType, LikeTypeDTO>()
                .ReverseMap();

            CreateMap<Rating, RatingDTO>()
                .ReverseMap();

            CreateMap<Image, ImageDTO>()
                .ReverseMap();
        }
    }
}
