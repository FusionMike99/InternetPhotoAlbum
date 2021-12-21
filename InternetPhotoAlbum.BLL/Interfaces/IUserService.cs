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
        Task<bool> UpdateAsync(EditUserProfileModel model);
        Task<bool> DeleteAsync(string id);
    }
}
