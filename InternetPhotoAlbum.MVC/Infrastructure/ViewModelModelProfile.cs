using AutoMapper;
using InternetPhotoAlbum.BLL.Models;
using InternetPhotoAlbum.MVC.Models;

namespace InternetPhotoAlbum.MVC.Infrastructure
{
    public class ViewModelModelProfile : Profile
    {
        public ViewModelModelProfile()
        {
            CreateMap<AlbumDTO, AlbumViewModel>()
                .ReverseMap();
        }
    }
}