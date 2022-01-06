using InternetPhotoAlbum.BLL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetPhotoAlbum.BLL.Interfaces
{
    /// <summary>
    /// Implementation of Service for LikeType
    /// </summary>
    public interface ILikeTypeService : IDisposable
    {
        /// <summary>
        /// Create new type of like
        /// </summary>
        /// <param name="model">Type of like being created</param>
        /// <returns>Created type of like</returns>
        Task<LikeTypeDTO> CreateAsync(LikeTypeDTO model);

        /// <summary>
        /// Find all types of like
        /// </summary>
        /// <returns>List of types of like</returns>
        IEnumerable<LikeTypeDTO> FindAll();

        /// <summary>
        /// Find type of like by identifier
        /// </summary>
        /// <param name="id">Type of like's identifier</param>
        /// <returns>Found type of like</returns>
        Task<LikeTypeDTO> FindByIdAsync(int id);

        /// <summary>
        /// Update type of like
        /// </summary>
        /// <param name="model">Updatable type of like</param>
        /// <returns>Operation result</returns>
        Task<bool> UpdateAsync(LikeTypeDTO model);

        /// <summary>
        /// Delete type of like
        /// </summary>
        /// <param name="id">Type of like's identifier</param>
        /// <returns>Operation result</returns>
        Task<bool> DeleteAsync(int id);
    }
}
