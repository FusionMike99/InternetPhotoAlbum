using InternetPhotoAlbum.BLL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetPhotoAlbum.BLL.Interfaces
{
    public interface IImageService : IDisposable
    {
        Task<ImageDTO> CreateAsync(ImageDTO model);
        IEnumerable<ImageDTO> FindAll();
        Task<ImageDTO> FindByIdAsync(int id);
        IEnumerable<ImageDTO> FindByAlbumId(int albumId);
        Task<bool> UpdateAsync(ImageDTO model);
        Task<bool> DeleteAsync(int id);
    }
}
