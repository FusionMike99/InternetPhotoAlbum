using InternetPhotoAlbum.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetPhotoAlbum.DAL.Interfaces
{
    public interface IRatingsRepository : IDisposable
    {
        Rating Create(Rating item);
        Task<Rating> GetByIdAsync(int imageId, string userId);
        IEnumerable<Rating> GetAll();
        void Update(Rating item);
        void Remove(Rating item);
    }
}
