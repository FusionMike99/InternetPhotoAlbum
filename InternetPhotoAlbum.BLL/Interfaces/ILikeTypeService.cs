using InternetPhotoAlbum.BLL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetPhotoAlbum.BLL.Interfaces
{
    public interface ILikeTypeService : IDisposable
    {
        Task<LikeTypeDTO> CreateAsync(LikeTypeDTO model);
        IEnumerable<LikeTypeDTO> FindAll();
        Task<LikeTypeDTO> FindByIdAsync(int id);
        Task<bool> UpdateAsync(LikeTypeDTO model);
        Task<bool> DeleteAsync(int id);
    }
}
