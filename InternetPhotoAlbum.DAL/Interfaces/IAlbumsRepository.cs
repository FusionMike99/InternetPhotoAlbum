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
        IEnumerable<Album> Get(Func<Album, bool> predicate);
        void Update(Album album);
        void Remove(Album album);
    }
}
