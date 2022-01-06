using InternetPhotoAlbum.DAL.Entities;
using Microsoft.AspNet.Identity;

namespace InternetPhotoAlbum.DAL.Identity
{
    /// <summary>
    /// User manager
    /// </summary>
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        /// <summary>
        /// Injection for User Store
        /// </summary>
        /// <param name="store">User store</param>
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
                : base(store)
        {
        }
    }
}
