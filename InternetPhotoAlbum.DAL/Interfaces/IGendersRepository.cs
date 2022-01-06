using InternetPhotoAlbum.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetPhotoAlbum.DAL.Interfaces
{
    /// <summary>
    /// Implementation of Repository pattern for entity Gender
    /// </summary>
    public interface IGendersRepository : IDisposable
    {
        /// <summary>
        /// Create new entity
        /// </summary>
        /// <param name="gender">Entity being created</param>
        /// <returns>Created entity</returns>
        Gender Create(Gender gender);

        /// <summary>
        /// Get entity by it's identifier
        /// </summary>
        /// <param name="id">Identifier of entity</param>
        /// <returns>Received entity</returns>
        Task<Gender> GetByIdAsync(int id);

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns>List of entities</returns>
        IEnumerable<Gender> GetAll();

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="gender">Updatable entity</param>
        void Update(Gender gender);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="gender">Entity being deleted</param>
        void Remove(Gender gender);
    }
}
