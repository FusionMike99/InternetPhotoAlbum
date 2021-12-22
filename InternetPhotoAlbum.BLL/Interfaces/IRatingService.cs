using InternetPhotoAlbum.BLL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetPhotoAlbum.BLL.Interfaces
{
    public interface IRatingService : IDisposable
    {
        Task<RatingDTO> CreateAsync(RatingDTO model);
        IEnumerable<RatingDTO> FindAll();
        Task<RatingDTO> FindByIdAsync(int imageId, string userId);
        Task<bool> UpdateAsync(RatingDTO model);
        Task<bool> DeleteAsync(int imageId, string userId);
    }
}
