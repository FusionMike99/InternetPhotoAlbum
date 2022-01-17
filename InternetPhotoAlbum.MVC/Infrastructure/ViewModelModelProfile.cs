using AutoMapper;
using InternetPhotoAlbum.BLL.Models;
using InternetPhotoAlbum.MVC.Models;
using System;
using System.IO;
using System.Web;

namespace InternetPhotoAlbum.MVC.Infrastructure
{
    /// <summary>
    /// Mapper profile between models and view models
    /// </summary>
    public class ViewModelModelProfile : Profile
    {
        /// <summary>
        /// Set up mapper profile
        /// </summary>
        public ViewModelModelProfile()
        {
            CreateMap<AlbumDTO, AlbumViewModel>()
                .ReverseMap();

            CreateMap<ImageDTO, IndexImageViewModel>()
                .ForMember(i => i.File, opt => opt.MapFrom(i => Convert.ToBase64String(i.File)));

            CreateMap<CreateImageViewModel, ImageDTO>()
                .ForMember(i => i.File, opt => opt.ConvertUsing(new ImageUploadFormatter(), src => src.File))
                .ForMember(i => i.ContentType, opt => opt.MapFrom(i => i.File.ContentType));

            CreateMap<ImageDTO, CreateImageViewModel>()
                .ForMember(i => i.File, opt => opt.Ignore());

            CreateMap<ImageDTO, EditImageViewModel>()
                .ReverseMap();

            CreateMap<UserDTO, IndexUserViewModel>()
                .ForMember(g => g.Gender, opt => opt.MapFrom(g => g.GenderName));

            CreateMap<RegisterModel, UserDTO>()
                .ForMember(u => u.Role, opt => opt.Ignore());
        }

        private class ImageUploadFormatter : IValueConverter<HttpPostedFileBase, byte[]>
        {
            public byte[] Convert(HttpPostedFileBase source, ResolutionContext context)
            {
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(source.InputStream))
                {
                    imageData = binaryReader.ReadBytes(source.ContentLength);
                }

                return imageData;
            }
        }
    }
}