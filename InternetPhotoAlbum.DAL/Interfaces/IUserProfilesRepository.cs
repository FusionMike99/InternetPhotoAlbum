using InternetPhotoAlbum.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetPhotoAlbum.DAL.Interfaces
{
    public interface IUserProfilesRepository : IDisposable
    {
        UserProfile Create(UserProfile profile);
        Task<UserProfile> GetByIdAsync(int id);
        IEnumerable<UserProfile> GetAll();
        void Update(UserProfile profile);
        void Remove(UserProfile profile);
    }
}
