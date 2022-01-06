using InternetPhotoAlbum.BLL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetPhotoAlbum.BLL.Interfaces
{
    /// <summary>
    /// Implementation of Service for Gender
    /// </summary>
    public interface IGenderService : IDisposable
    {
        /// <summary>
        /// Create new gender
        /// </summary>
        /// <param name="model">Gender being created</param>
        /// <returns>Created gender</returns>
        Task<GenderDTO> CreateAsync(GenderDTO model);

        /// <summary>
        /// Find all genders
        /// </summary>
        /// <returns>List of genders</returns>
        IEnumerable<GenderDTO> FindAll();

        /// <summary>
        /// Find gender by identifier
        /// </summary>
        /// <param name="id">Gender's identifier</param>
        /// <returns>Found gender</returns>
        Task<GenderDTO> FindByIdAsync(int id);

        /// <summary>
        /// Update gender
        /// </summary>
        /// <param name="model">Updatable gender</param>
        /// <returns>Operation result</returns>
        Task<bool> UpdateAsync(GenderDTO model);

        /// <summary>
        /// Delete gender
        /// </summary>
        /// <param name="id">Gender's identifier</param>
        /// <returns>Operation result</returns>
        Task<bool> DeleteAsync(int id);
    }
}
