using InternetPhotoAlbum.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetPhotoAlbum.DAL.Interfaces
{
    public interface IGendersRepository : IDisposable
    {
        Gender Create(Gender gender);
        Task<Gender> GetByIdAsync(int id);
        IEnumerable<Gender> GetAll();
        void Update(Gender gender);
        void Remove(Gender gender);
    }
}
