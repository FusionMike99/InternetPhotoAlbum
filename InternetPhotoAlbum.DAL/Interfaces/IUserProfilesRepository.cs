using InternetPhotoAlbum.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetPhotoAlbum.DAL.Interfaces
{
    /// <summary>
    /// Implementation of Repository pattern for entity UserProfile
    /// </summary>
    public interface IUserProfilesRepository : IDisposable
    {
        /// <summary>
        /// Create new entity
        /// </summary>
        /// <param name="profile">Entity being created</param>
        /// <returns>Created entity</returns>
        UserProfile Create(UserProfile profile);

        /// <summary>
        /// Get entity by it's identifier
        /// </summary>
        /// <param name="id">Identifier of entity</param>
        /// <returns>Received entity</returns>
        Task<UserProfile> GetByIdAsync(string id);

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns>List of entities</returns>
        IEnumerable<UserProfile> GetAll();

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="profile">Updatable entity</param>
        void Update(UserProfile profile);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="profile">Entity being deleted</param>
        void Remove(UserProfile profile);
    }
}
