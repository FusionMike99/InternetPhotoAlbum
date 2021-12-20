using InternetPhotoAlbum.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetPhotoAlbum.DAL.Interfaces
{
    public interface IAlbumsRepository : IDisposable
    {
        Album Create(Album album);
        Task<Album> GetByIdAsync(int id);
        IEnumerable<Album> GetAll();
        void Update(Album album);
        void Remove(Album album);
    }
}
