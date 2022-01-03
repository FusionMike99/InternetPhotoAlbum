using InternetPhotoAlbum.BLL.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InternetPhotoAlbum.BLL.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<ClaimsIdentity> AuthenticateAsync(LoginModel model);
        Task<UserDTO> CreateAsync(UserDTO model);
        IEnumerable<UserDTO> FindAll();
        Task<UserDTO> FindByIdAsync(string id);
        Task LockUser(string userId, bool isLocked);
        Task<bool> UpdateAsync(EditUserProfileModel model);
        Task<bool> ChangePassword(string userId, string oldPassword, string newPassword);
        Task<bool> DeleteAsync(string id);
    }
}
