using InternetPhotoAlbum.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetPhotoAlbum.DAL.Interfaces
{
    public interface ILikeTypesRepository : IDisposable
    {
        LikeType Create(LikeType type);
        Task<LikeType> GetByIdAsync(int id);
        IEnumerable<LikeType> GetAll();
        void Update(LikeType type);
        void Remove(LikeType type);
    }
}
