using InternetPhotoAlbum.BLL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetPhotoAlbum.BLL.Interfaces
{
    public interface IAlbumService : IDisposable
    {
        Task<AlbumDTO> CreateAsync(AlbumDTO model);
        IEnumerable<AlbumDTO> FindAll();
        Task<AlbumDTO> FindByIdAsync(int id);
        IEnumerable<AlbumDTO> FindByUserId(string userId);
        Task<bool> UpdateAsync(AlbumDTO model);
        Task<bool> DeleteAsync(int id);
    }
}
