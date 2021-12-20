using InternetPhotoAlbum.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetPhotoAlbum.DAL.Interfaces
{
    public interface IImagesRepository : IDisposable
    {
        Image Create(Image image);
        Task<Image> GetByIdAsync(int id);
        IEnumerable<Image> GetAll();
        void Update(Image image);
        void Remove(Image image);
    }
}
