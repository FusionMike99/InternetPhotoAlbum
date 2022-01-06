using InternetPhotoAlbum.BLL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetPhotoAlbum.BLL.Interfaces
{
    /// <summary>
    /// Implementation of Service for Album
    /// </summary>
    public interface IAlbumService : IDisposable
    {
        /// <summary>
        /// Create new album
        /// </summary>
        /// <param name="model">Album being created</param>
        /// <returns>Created album</returns>
        Task<AlbumDTO> CreateAsync(AlbumDTO model);

        /// <summary>
        /// Find all albums
        /// </summary>
        /// <returns>List of albums</returns>
        IEnumerable<AlbumDTO> FindAll();

        /// <summary>
        /// Find album by identifier
        /// </summary>
        /// <param name="id">Album's identifier</param>
        /// <returns>Found album</returns>
        Task<AlbumDTO> FindByIdAsync(int id);

        /// <summary>
        /// Find albums by user's identifier
        /// </summary>
        /// <param name="userId">User's identifier</param>
        /// <returns>List of albums</returns>
        IEnumerable<AlbumDTO> FindByUserId(string userId);

        /// <summary>
        /// Update album
        /// </summary>
        /// <param name="model">Updatable album</param>
        /// <returns>Operation result</returns>
        Task<bool> UpdateAsync(AlbumDTO model);

        /// <summary>
        /// Delete album
        /// </summary>
        /// <param name="id">Album's identifier</param>
        /// <returns>Operation result</returns>
        Task<bool> DeleteAsync(int id);
    }
}
