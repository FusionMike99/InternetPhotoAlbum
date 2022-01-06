using InternetPhotoAlbum.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetPhotoAlbum.DAL.Interfaces
{
    /// <summary>
    /// Implementation of Repository pattern for entity LikeType
    /// </summary>
    public interface ILikeTypesRepository : IDisposable
    {
        /// <summary>
        /// Create new entity
        /// </summary>
        /// <param name="type">Entity being created</param>
        /// <returns>Created entity</returns>
        LikeType Create(LikeType type);

        /// <summary>
        /// Get entity by it's identifier
        /// </summary>
        /// <param name="id">Identifier of entity</param>
        /// <returns>Received entity</returns>
        Task<LikeType> GetByIdAsync(int id);

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns>List of entities</returns>
        IEnumerable<LikeType> GetAll();

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="type">Updatable entity</param>
        void Update(LikeType type);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="type">Entity being deleted</param>
        void Remove(LikeType type);
    }
}
