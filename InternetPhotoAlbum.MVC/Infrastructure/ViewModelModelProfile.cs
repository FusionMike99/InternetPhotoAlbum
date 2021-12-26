using AutoMapper;
using InternetPhotoAlbum.BLL.Models;
using InternetPhotoAlbum.MVC.Models;
using System;
using System.IO;
using System.Web;

namespace InternetPhotoAlbum.MVC.Infrastructure
{
    public class ViewModelModelProfile : Profile
    {
        public ViewModelModelProfile()
        {
            CreateMap<AlbumDTO, AlbumViewModel>()
                .ReverseMap();

            CreateMap<ImageDTO, IndexImageViewModel>()
                .ForMember(i => i.File, x => x.MapFrom(i => Convert.ToBase64String(i.File)));

            CreateMap<CreateImageViewModel, ImageDTO>()
                .ForMember(i => i.File, x => x.ConvertUsing(new ImageUploadFormatter(), src => src.File));

            CreateMap<ImageDTO, CreateImageViewModel>()
                .ForMember(i => i.File, x => x.Ignore());

            CreateMap<EditImageViewModel, CreateImageViewModel>()
                .ForMember(i => i.File, x => x.Ignore());
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