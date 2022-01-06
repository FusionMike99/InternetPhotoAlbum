using InternetPhotoAlbum.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetPhotoAlbum.DAL.Interfaces
{
    /// <summary>
    /// Implementation of Repository pattern for entity Image
    /// </summary>
    public interface IImagesRepository : IDisposable
    {
        /// <summary>
        /// Create new entity
        /// </summary>
        /// <param name="image">Entity being created</param>
        /// <returns>Created entity</returns>
        Image Create(Image image);

        /// <summary>
        /// Get entity by it's identifier
        /// </summary>
        /// <param name="id">Identifier of entity</param>
        /// <returns>Received entity</returns>
        Task<Image> GetByIdAsync(int id);

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns>List of entities</returns>
        IEnumerable<Image> GetAll();

        /// <summary>
        /// Get entities by predicate
        /// </summary>
        /// <param name="predicate">Predicate</param>
        /// <returns>List of entities</returns>
        IEnumerable<Image> Get(Func<Image, bool> predicate);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="image">Updatable entity</param>
        void Update(Image image);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="image">Entity being deleted</param>
        void Remove(Image image);
    }
}
