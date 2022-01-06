using InternetPhotoAlbum.BLL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetPhotoAlbum.BLL.Interfaces
{
    /// <summary>
    /// Implementation of Service for Image
    /// </summary>
    public interface IImageService : IDisposable
    {
        /// <summary>
        /// Create new image
        /// </summary>
        /// <param name="model">Image being created</param>
        /// <returns>Created image</returns>
        Task<ImageDTO> CreateAsync(ImageDTO model);

        /// <summary>
        /// Find all images
        /// </summary>
        /// <returns>List of images</returns>
        IEnumerable<ImageDTO> FindAll();

        /// <summary>
        /// Find image by identifier
        /// </summary>
        /// <param name="id">Image's identifier</param>
        /// <returns>Found image</returns>
        Task<ImageDTO> FindByIdAsync(int id);

        /// <summary>
        /// Find images by album's identifier
        /// </summary>
        /// <param name="albumId">Album's identifier</param>
        /// <returns>List of images</returns>
        IEnumerable<ImageDTO> FindByAlbumId(int albumId);

        /// <summary>
        /// Find images by its title
        /// </summary>
        /// <param name="title">Image's title</param>
        /// <returns>List of images</returns>
        IEnumerable<ImageDTO> FindByTitle(string title);

        /// <summary>
        /// Update image
        /// </summary>
        /// <param name="model">Updatable image</param>
        /// <returns>Operation result</returns>
        Task<bool> UpdateAsync(ImageDTO model);

        /// <summary>
        /// Delete image
        /// </summary>
        /// <param name="id">Image's identifier</param>
        /// <returns>Operation result</returns>
        Task<bool> DeleteAsync(int id);
    }
}
