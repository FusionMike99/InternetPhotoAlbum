using InternetPhotoAlbum.BLL.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InternetPhotoAlbum.BLL.Interfaces
{
    /// <summary>
    /// Implementation of Service for User
    /// </summary>
    public interface IUserService : IDisposable
    {
        /// <summary>
        /// Authenticate user
        /// </summary>
        /// <param name="model">Data for authenticating</param>
        /// <returns>User's claims</returns>
        Task<ClaimsIdentity> AuthenticateAsync(LoginModel model);

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="model">User being created</param>
        /// <returns>Created user</returns>
        Task<UserDTO> CreateAsync(UserDTO model);

        /// <summary>
        /// Find all users
        /// </summary>
        /// <returns>List of users</returns>
        IEnumerable<UserDTO> FindAll();

        /// <summary>
        /// Find user by identifier
        /// </summary>
        /// <param name="id">User's identifier</param>
        /// <returns>Found user</returns>
        Task<UserDTO> FindByIdAsync(string id);

        /// <summary>
        /// Lock user and its albums, images
        /// </summary>
        /// <param name="userId">User's identifier</param>
        /// <param name="isLocked">Unlock or lock user</param>
        /// <returns></returns>
        Task LockUser(string userId, bool isLocked);

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="model">Updatable user</param>
        /// <returns>Operation result</returns>
        Task<bool> UpdateAsync(EditUserProfileModel model);

        /// <summary>
        /// Change user's password
        /// </summary>
        /// <param name="userId">User's identifier</param>
        /// <param name="oldPassword">Old user's password</param>
        /// <param name="newPassword">New user's password</param>
        /// <returns>Operation result</returns>
        Task<bool> ChangePassword(string userId, string oldPassword, string newPassword);

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="id">User's identifier</param>
        /// <returns>Operation result</returns>
        Task<bool> DeleteAsync(string id);
    }
}
