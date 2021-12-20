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
        IGendersRepository GendersRepository { get; }
        IImagesRepository ImagesRepository { get; }
        ILikeTypesRepository LikeTypesRepository { get; }
        IRatingsRepository RatingsRepository { get; }
        IUserProfilesRepository UserProfilesRepository { get; }
        Task<int> SaveAsync();
    }
}
