using InternetPhotoAlbum.BLL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetPhotoAlbum.BLL.Interfaces
{
    public interface IGenderService : IDisposable
    {
        Task<GenderDTO> CreateAsync(GenderDTO model);
        IEnumerable<GenderDTO> FindAll();
        Task<GenderDTO> FindByIdAsync(int id);
        Task<bool> UpdateAsync(GenderDTO model);
        Task<bool> DeleteAsync(int id);
    }
}
