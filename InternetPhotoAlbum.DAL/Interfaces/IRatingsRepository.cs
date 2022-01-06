using InternetPhotoAlbum.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetPhotoAlbum.DAL.Interfaces
{
    /// <summary>
    /// Implementation of Repository pattern for entity Rating
    /// </summary>
    public interface IRatingsRepository : IDisposable
    {
        /// <summary>
        /// Create new entity
        /// </summary>
        /// <param name="item">Entity being created</param>
        /// <returns>Created entity</returns>
        Rating Create(Rating item);

        /// <summary>
        /// Get entity by it's identifiers
        /// </summary>
        /// <param name="imageId">Identifier of image</param>
        /// <param name="userId">Identifier of user</param>
        /// <returns>Received entity</returns>
        Task<Rating> GetByIdAsync(int imageId, string userId);

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns>List of entities</returns>
        IEnumerable<Rating> GetAll();

        /// <summary>
        /// Get entities by predicate
        /// </summary>
        /// <param name="predicate">Predicate</param>
        /// <returns>List of entities</returns>
        IEnumerable<Rating> Get(Func<Rating, bool> predicate);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="item">Updatable entity</param>
        void Update(Rating item);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="item">Entity being deleted</param>
        void Remove(Rating item);
    }
}
