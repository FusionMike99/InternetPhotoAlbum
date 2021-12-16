using InternetPhotoAlbum.DAL.Identity;
using System;
using System.Threading.Tasks;

namespace InternetPhotoAlbum.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ApplicationUserManager UserManager { get; }
        ApplicationRoleManager RoleManager { get; }
        IAlbumsRepository AlbumsRepository { get; }
        IImagesRepository ImagesRepository { get; }
        IRatingsRepository RatingsRepository { get; }
        Task<int> SaveAsync();
    }
}
