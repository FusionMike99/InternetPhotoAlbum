using InternetPhotoAlbum.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetPhotoAlbum.DAL.Interfaces
{
    /// <summary>
    /// Implementation of Repository pattern for entity Album
    /// </summary>
    public interface IAlbumsRepository : IDisposable
    {
        /// <summary>
        /// Create new entity
        /// </summary>
        /// <param name="album">Entity being created</param>
        /// <returns>Created entity</returns>
        Album Create(Album album);

        /// <summary>
        /// Get entity by it's identifier
        /// </summary>
        /// <param name="id">Identifier of entity</param>
        /// <returns>Received entity</returns>
        Task<Album> GetByIdAsync(int id);

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns>List of entities</returns>
        IEnumerable<Album> GetAll();

        /// <summary>
        /// Get entities by predicate
        /// </summary>
        /// <param name="predicate">Predicate</param>
        /// <returns>List of entities</returns>
        IEnumerable<Album> Get(Func<Album, bool> predicate);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="album">Updatable entity</param>
        void Update(Album album);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="album">Entity being deleted</param>
        void Remove(Album album);
    }
}
