using AutoMapper;
using InternetPhotoAlbum.BLL.Models;
using InternetPhotoAlbum.DAL.Entities;

namespace InternetPhotoAlbum.BLL.Infrastructure
{
    /// <summary>
    /// Mapper profile between entities and models
    /// </summary>
    public class ModelEntityProfile : Profile
    {
        /// <summary>
        /// Set up mapper profile
        /// </summary>
        public ModelEntityProfile()
        {
            CreateMap<UserProfile, UserDTO>()
                .ForMember(u => u.Id, opt => opt.MapFrom(up => up.UserId))
                .ForMember(u => u.GenderName, opt => opt.MapFrom(up => up.Gender.Name));

            CreateMap<UserDTO, UserProfile>()
                .ForMember(u => u.UserId, opt => opt.MapFrom(up => up.Id));

            CreateMap<ApplicationUser, UserDTO>()
                .ForMember(u => u.Name, opt => opt.MapFrom(up => up.UserProfile.Name))
                .ForMember(u => u.Login, opt => opt.MapFrom(up => up.UserName))
                .ForMember(u => u.Surname, opt => opt.MapFrom(up => up.UserProfile.Surname))
                .ForMember(u => u.DateOfBirth, opt => opt.MapFrom(up => up.UserProfile.DateOfBirth))
                .ForMember(u => u.IsLocked, opt => opt.MapFrom(up => up.LockoutEnabled))
                .ForMember(u => u.GenderId, opt => opt.MapFrom(up => up.UserProfile.GenderId))
                .ForMember(u => u.GenderName, opt => opt.MapFrom(up => up.UserProfile.Gender.Name));

            CreateMap<UserDTO, EditUserProfileModel>();

            CreateMap<EditUserProfileModel, UserProfile>()
                .ForMember(u => u.UserId, opt => opt.MapFrom(u => u.Id));

            CreateMap<Album, AlbumDTO>()
                .ForMember(a => a.UserName, opt => opt.MapFrom(a => a.User.UserName))
                .ReverseMap();

            CreateMap<Gender, GenderDTO>()
                .ReverseMap();

            CreateMap<LikeType, LikeTypeDTO>()
                .ReverseMap();

            CreateMap<Rating, RatingDTO>()
                .ReverseMap();

            CreateMap<Image, ImageDTO>();

            CreateMap<ImageDTO, Image>()
                .ForMember(i => i.Album, opt => opt.Ignore());
        }
    }
}
