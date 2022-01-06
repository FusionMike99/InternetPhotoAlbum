using InternetPhotoAlbum.DAL.Identity;
using System;
using System.Threading.Tasks;

namespace InternetPhotoAlbum.DAL.Interfaces
{
    /// <summary>
    /// Implementation of Unit of Work pattern
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        ApplicationUserManager UserManager { get; }
        ApplicationRoleManager RoleManager { get; }
        IAlbumsRepository AlbumsRepository { get; }
        IGendersRepository GendersRepository { get; }
        IImagesRepository ImagesRepository { get; }
        ILikeTypesRepository LikeTypesRepository { get; }
        IRatingsRepository RatingsRepository { get; }
        IProceduresRepository ProceduresRepository { get; }
        IUserProfilesRepository UserProfilesRepository { get; }

        /// <summary>
        /// Save current context to database
        /// </summary>
        /// <returns>Number of records affected</returns>
        Task<int> SaveAsync();
    }
}
