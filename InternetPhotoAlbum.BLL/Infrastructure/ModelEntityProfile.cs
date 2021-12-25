﻿using AutoMapper;
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
