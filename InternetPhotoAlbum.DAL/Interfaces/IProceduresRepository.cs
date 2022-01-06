using System;

namespace InternetPhotoAlbum.DAL.Interfaces
{
    /// <summary>
    /// Implementation of Repository pattern for Stored Procedures of Database
    /// </summary>
    public interface IProceduresRepository : IDisposable
    {
        /// <summary>
        /// Lock user's account
        /// </summary>
        /// <param name="userId">User's identifier</param>
        /// <param name="isLocked">Unlock or lock user</param>
        void LockUser(string userId, bool isLocked);
    }
}
